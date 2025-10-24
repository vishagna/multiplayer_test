using Coherence.Toolkit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalText : MonoBehaviour
{
    [SerializeField] private Button activeMultiTextButton;
    [SerializeField] private TMP_Text singleGlobalText;
    [SerializeField] private Transform multiGlobalText;
    [SerializeField] private Transform inputTextField;
    [SerializeField] private TMP_Text textField;

    [SerializeField] private List<string> globalTexts;

    private void Start()
    {
        activeMultiTextButton.onClick.RemoveAllListeners();
        activeMultiTextButton.onClick.AddListener(() =>
        {
            activeMultiTextButton.gameObject.SetActive(false);
            multiGlobalText.gameObject.SetActive(true);
            inputTextField.gameObject.SetActive(true);
        });

        multiGlobalText.GetComponent<Button>().onClick.RemoveAllListeners();
        multiGlobalText.GetComponent<Button>().onClick.AddListener(() =>
        {
            activeMultiTextButton.gameObject.SetActive(true);
            multiGlobalText.gameObject.SetActive(false);
            inputTextField.gameObject.SetActive(false);

        });
    }

    private void Update()
    {
        if (GlobalNotification.Instance != null)
        {
            globalTexts = GlobalNotification.Instance.GetGlobalText();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SendChat($"<color=#FF0000>{GameManager.Instance.PlayerName}</color>: <color=#FFFFFF>{textField.text}</color>");
        }
        if (globalTexts.Count == 0) return;
        singleGlobalText.text = globalTexts.Last();

        foreach (Transform child in multiGlobalText)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = globalTexts.Count - 1; i >= 0; i--)
        {
            TMP_Text newText = Instantiate(singleGlobalText, multiGlobalText);
            newText.text = globalTexts[i];
        }


    }

    private void HandleNewMessage(string message)
    {
        globalTexts.Add(message);
        if (globalTexts.Count > 7)
        {
            globalTexts.RemoveAt(0);
        }
    }



    private void SendChat(string message)
    {
        if (!string.IsNullOrWhiteSpace(message) && GlobalNotification.Instance != null)
        {
            GlobalNotification.Instance.SendChatMessage(message);
        }
    }

    void OnEnable()
    {
        // Đăng ký nhận tin nhắn mới từ GlobalNotification
        GlobalNotification.OnMessageReceived += HandleNewMessage;
    }

    void OnDisable()
    {
        // Hủy đăng ký khi object bị disable/destroy
        GlobalNotification.OnMessageReceived -= HandleNewMessage;
    }


}
