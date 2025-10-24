using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex;

    public int TargetSceneIndex => targetSceneIndex;
}
