using System.Collections;
using TMPro;
using UnityEngine;

public class QuickCharacterInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterHP;
    [SerializeField] private TMP_Text characterATK;

    Coroutine coroutine;

    public void SetCharacterInfo(string name, int hp, int atk)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(closeInfo());
        characterName.text = name;
        characterHP.text = "HP: " + hp.ToString();
        characterATK.text = "ATK: " + atk.ToString();
    }

    IEnumerator closeInfo()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
