using Unity.Cinemachine;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public static CharacterCamera Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private Transform cameraObject;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    public void SetTrackingTarget(Transform target)
    {
        cinemachineCamera.Follow = target;
        cinemachineCamera.LookAt = target;
    }
}
