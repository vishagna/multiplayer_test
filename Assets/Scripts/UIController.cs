using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private QuickCharacterInformation characterInfoUI;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked on: " + clickedObject.name);

                CharacterController character = clickedObject.GetComponent<CharacterController>();
                if (character != null)
                {
                    characterInfoUI.gameObject.SetActive(true);
                    characterInfoUI.SetCharacterInfo(character.characterName, character.Hp, character.Atk);
                }
                else
                {
                    Debug.Log("❌ Object này không có CharacterController");
                }
            }
        }
    }
}
