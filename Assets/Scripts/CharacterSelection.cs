using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [ReadOnly][SerializeField] private int selectionIndex = 0;
    [SerializeField] private List<SelectiveCharacter> characterLists;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] GameObject characterSelectable;


    private float targetAngle = 0f;
    private float currentAngle = 0f;

    void Start()
    {
        characterLists = new List<SelectiveCharacter>();
        for (int i = 0; i < GameManager.Instance.GetCharacterDataList().Count; i++)
        {
            SelectiveCharacter selectiveCharacter = new SelectiveCharacter(GameManager.Instance.GetCharacterDataList()[i]);
            characterLists.Add(selectiveCharacter);
        }
        Transform parentTransform = transform;
        int count = characterLists.Count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );
            GameObject character = Instantiate(
                characterSelectable,
                position,
                Quaternion.identity,
                parentTransform
            );
            character.GetComponent<CharacterSelectable>().SetCharacterData(characterLists[i].GetCharacterData());
            character.AddComponent<BillboardChild>();
            characterLists[i].SetCharacterObject(character);
        }
    }

    void Update()
    {
        GameManager.Instance.SetSelectedCharacterIndex(selectionIndex);
        if (Mathf.Abs(targetAngle - currentAngle) > 0.01f)
        {
            currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * 5f);
            UpdateCharacterPositions();
        }
    }

    public CharacterData SwitchCharacter(int direction)
    {
        Debug.Log("SwitchCharacter direction: " + direction);
        selectionIndex = (selectionIndex + (direction == 0 ? -1 : 1) + characterLists.Count) % characterLists.Count;
        float angleStep = 360f / characterLists.Count;
        targetAngle += (direction == 0 ? angleStep : -angleStep);
        return characterLists[selectionIndex].GetCharacterData();
    }

    private void UpdateCharacterPositions()
    {
        int count = characterLists.Count;
        for (int i = 0; i < count; i++)
        {
            float angle = (i * Mathf.PI * 2f / count) + Mathf.Deg2Rad * currentAngle;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            characterLists[i].GetCharacterObject().transform.localPosition = pos;
        }
    }
}

[System.Serializable]
public class SelectiveCharacter
{
    public SelectiveCharacter(CharacterData _characterData) 
    {
        characterData = _characterData;
    }
    [SerializeField] private CharacterData characterData;
    [SerializeField] private GameObject characterObject;

    public CharacterData GetCharacterData() => characterData;
    public GameObject GetCharacterObject() => characterObject;

    public void SetCharacterObject(GameObject obj) => characterObject = obj;
}
