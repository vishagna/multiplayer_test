using System.Collections.Generic;
using Coherence.Connection;
using Coherence.Toolkit;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private List<CharacterData> characterDataList;
    [SerializeField] private int selectedCharacterIndex = 0;
    [SerializeField] private string playerName = "Player";
    public string PlayerName => playerName;
    public void SetPlayerName(string pName)
    {
        playerName = pName;
    }
    public int SelectedCharacterIndex => selectedCharacterIndex;

    public List<CharacterData> GetCharacterDataList()
    {
        return characterDataList;
    }
    public CharacterData GetSelectedCharacterData()
    {
        return GetCharacterData(selectedCharacterIndex);
    }
    public CharacterData GetCharacterData(int pIndex)
    {
        if (characterDataList != null && characterDataList.Count > pIndex)
        {
            return characterDataList[pIndex];
        }
        return null;
    }

    public void SetSelectedCharacterIndex(int pIndex)
    {
        selectedCharacterIndex = pIndex;
    }

}