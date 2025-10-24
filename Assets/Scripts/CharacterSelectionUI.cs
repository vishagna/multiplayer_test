using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] CharacterSelection characterContainer;
    [SerializeField] private TMP_Text playerName;

    void Start()
    {
        leftButton.onClick.RemoveAllListeners();
        leftButton.onClick.AddListener(() => {
            characterContainer.SwitchCharacter(0);
        });

        rightButton.onClick.RemoveAllListeners();
        rightButton.onClick.AddListener(() => {
            characterContainer.SwitchCharacter(1);
        });
    }

    void Update()
    {
        characterName.text = GameManager.Instance.GetSelectedCharacterData().GetCharacterName();
        GameManager.Instance.SetPlayerName(playerName.text);

    }
}
