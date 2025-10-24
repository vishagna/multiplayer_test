using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string characterName;
    public string CharacterName => characterName;
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private int attackPower;
    public int AttackPower => attackPower;

    [SerializeField] private Mesh characterMesh;

    [SerializeField] private Material characterMaterial;
    public Material CharacterMaterial => characterMaterial;
    public Mesh CharacterMesh => characterMesh;


    public string GetCharacterName()
    {
        return characterName;
    }

 
}
