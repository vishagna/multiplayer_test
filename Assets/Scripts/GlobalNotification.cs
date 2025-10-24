using Coherence;
using Coherence.Toolkit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoherenceSync))]
public class GlobalNotification : MonoBehaviour
{
    public static GlobalNotification Instance { get; private set; }
    private CoherenceSync sync;

    public static event Action<string> OnMessageReceived;

    [SerializeField] List<string> messages;
    public List<string> GetGlobalText()
    {
        return messages;
    }

    void Awake()
    {
        sync = GetComponent<CoherenceSync>();
        if (sync == null)
        {
            Debug.LogError("GlobalNotification yêu cầu CoherenceSync!");
            return;
        }


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            return;
        }
    }


    public void SendChatMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        sync.SendCommand<GlobalNotification>(nameof(RequestBroadcastChatMessage), MessageTarget.AuthorityOnly, message);
    }


    [Command]
    public void RequestBroadcastChatMessage(string message)
    {
        Debug.Log($"[SERVER nhận chat] {message}");

        sync.SendCommand<GlobalNotification>(nameof(ReceiveChatMessage), MessageTarget.All, message);
    }




    [Command(defaultRouting = MessageTarget.All)]
    public void ReceiveChatMessage(string message)
    {
        Debug.Log($"[CHAT NHẬN] {name}");
        if (messages.Count > 7)
        {
            messages.RemoveAt(0);
        }
        OnMessageReceived?.Invoke(message);
    }
}
