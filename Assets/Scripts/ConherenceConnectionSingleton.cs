using Coherence.Toolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;

public class ConherenceConnectionSingleton : MonoBehaviour
{
    [SerializeField] private CoherenceSync characterThis;
    private CoherenceBridge bridge;

    private void Start()
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

    private void Update()
    {

    }

    private IEnumerator LoadNextScene(int sceneIndex)
    {

        CoherenceSync[] bringAlong = new CoherenceSync[] { characterThis };
        yield return CoherenceSceneManager.LoadScene(bridge, sceneIndex, bringAlong);
    }
}
