using UnityEngine;

public class BillboardChild : MonoBehaviour
{
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private bool isFlip = false;

    void Update()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            transform.LookAt(mainCameraTransform);
            if (isFlip)
            {
                Quaternion flipRotation = Quaternion.Euler(0, 180, 0);
                transform.localRotation *= flipRotation;
            }
        }
    }
}