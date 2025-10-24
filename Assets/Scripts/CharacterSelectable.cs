using UnityEngine;

public class CharacterSelectable : MonoBehaviour
{
    public CharacterData characterData;
    [SerializeField] private SkinnedMeshRenderer characterMeshRenderer;

    public void SetCharacterData(CharacterData pcD)
    {
        characterData = pcD;
        characterMeshRenderer.sharedMesh = characterData.CharacterMesh;
        characterMeshRenderer.material = characterData.CharacterMaterial;
    }
}
