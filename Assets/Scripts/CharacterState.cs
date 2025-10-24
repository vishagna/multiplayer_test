using System.Collections;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] GameObject modelObject;
    [SerializeField] private GameObject tombObject;
    private Coroutine reviveCoroutine;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            characterController.TakeDamage(39);
        }
        if (characterController.IsDead == true)
        {
            if (reviveCoroutine != null) return;
            modelObject.SetActive(false);
            tombObject.SetActive(true);
            reviveCoroutine = StartCoroutine(castRevive());
        }
    }
    IEnumerator castRevive()
    {
        yield return new WaitForSeconds(5f);
        modelObject.SetActive(true);
        tombObject.SetActive(false);
        characterController.Hp = characterController.characterData.Health;
        characterController.IsDead = false;

    }

}