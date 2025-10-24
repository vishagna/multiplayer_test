using UnityEngine;

public class ModelUpdating : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    void Update()
    {
        Mesh mesh = GameManager.Instance.GetCharacterData(characterController.CharIndex).CharacterMesh;
        GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        GetComponent<SkinnedMeshRenderer>().material = GameManager.Instance.GetCharacterData(characterController.CharIndex).CharacterMaterial;
    }
}
