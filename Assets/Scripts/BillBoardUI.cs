using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform followTarget; 
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0); 

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null || followTarget == null) return;

        transform.position = followTarget.position + offset;

        Quaternion camRot = mainCamera.transform.rotation;
        Vector3 euler = camRot.eulerAngles;
        euler.z = 0;
        transform.rotation = Quaternion.Euler(euler);
    }
}
