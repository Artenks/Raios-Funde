using System;
using System.Collections.Generic;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;

    public TwitchTags TwitchTags;

    public GameObject ChatPrefab;
    public GameObject ChatSystem;
    public GameObject ChatMessages;

    public string SystemMessage;

    [Serializable]
    public struct Infos
    {
        public string DisplayName;
        public string Message;
        public string Color;
        public string Badges;
        public bool Subscriber;
        public bool Mod;
        public bool Vip;
        public bool Broadcaster;
        public bool Pleb;
    }
    public List<Infos> UsersInfo;

    public Infos RecentUserInfo;
    public bool SendNewChatMessage = true;

    [SerializeField]
    private List<GameObject> chatList = new List<GameObject>();

    public void OnChatMessage(string _, string pMessage)
    {
        if (pMessage == "")
            return;

        if (pMessage.Length > 130)
            return;

        UsersInfo.Add(new Infos
        {
            DisplayName = TwitchTags.Tags.DisplayName,
            Message = TwitchTags.Tags.Message,
            Color = TwitchTags.Tags.Color,
            Subscriber = TwitchTags.Tags.Subscriber,
            Mod = TwitchTags.Tags.Mod,
            Vip = TwitchTags.Tags.Vip,
            Broadcaster = TwitchTags.Tags.Broadcaster,
            Pleb = TwitchTags.Tags.Pleb,
        });
    }
    public void OnChatSystemMessage(string systemMessage)
    {
        ClearChat();
        SystemMessage = systemMessage;
        chatList.Add(Instantiate(ChatSystem, ChatMessages.GetComponent<RectTransform>().position, Quaternion.identity, ChatMessages.transform));
    }

    private void Update()
    {
        if (UsersInfo.Count > 0 && SendNewChatMessage)
        {
            SendNewChatMessage = false;
            RecentUserInfo = UsersInfo[0];
            ClearChat();
            chatList.Add(Instantiate(ChatPrefab, ChatMessages.GetComponent<RectTransform>().position, Quaternion.identity, ChatMessages.transform));
        }
    }

    private void ClearOnRestart()
    {
        for (var i = chatList.Count - 1; i >= 0; i--)
        {
            Destroy(chatList[i]);
        }
        chatList.Clear();
    }

    private void ClearChat()
    {
        if (chatList.Count >= 20)
        {
            Destroy(chatList[0]);
            chatList.RemoveAt(0);
        }
    }

    private void Awake()
    {
        ConnectOnTwitch.ChatListEventHandler += ConnectOnTwitch_ChatListEventHandler;
    }

    private void ConnectOnTwitch_ChatListEventHandler()
    {
        UsersInfo.Clear();
        SendNewChatMessage = true;
    }
}
