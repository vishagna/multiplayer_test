using Coherence.Toolkit;
using System.Collections;
using UnityEngine;

public class CharacterTriggerCheck : MonoBehaviour
{

    [SerializeField] private CoherenceSync characterThis;
    private CoherenceBridge bridge;
    [SerializeField] private CharacterController characterController;

    void Start()
    {
        bridge = FindAnyObjectByType<CoherenceBridge>();
        if (bridge != null)
        {
            bridge.ClientConnections.OnSynced += connectionManager =>
            {
                StartCoroutine(LoadNextScene(1));
            };
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warp"))
        {
            WarpGate warpGate = other.GetComponent<WarpGate>();
            StartCoroutine(LoadNextScene(warpGate.TargetSceneIndex));
            Debug.Log("Character đã vào vùng trigger!");

        }

        if (other.CompareTag("Axe"))
        {
            characterController.TakeDamage(30);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Warp"))
        {
            Debug.Log("Character đang trong vùng trigger!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Warp"))
        {
            Debug.Log("Character đã rời vùng trigger!");
        }
    }

    private IEnumerator LoadNextScene(int sceneIndex)
    {

        CoherenceSync[] bringAlong = new CoherenceSync[] { characterThis};
        yield return CoherenceSceneManager.LoadScene(bridge, sceneIndex, bringAlong);
    }
}
