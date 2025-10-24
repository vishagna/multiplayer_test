using TMPro;
using UnityEngine;

public class PlayerNameUpdating : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    TMP_Text playerNameText;
    void Update()
    {
        playerNameText = GetComponent<TMP_Text>();
        playerNameText.text = characterController.PlayerName;
    }
}
