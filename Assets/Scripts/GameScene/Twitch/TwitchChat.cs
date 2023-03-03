using System.Collections.Generic;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;

    public TwitchTags TwitchTags;

    public GameObject ChatPrefab;
    public GameObject ChatMessages;

    [SerializeField]
    private List<GameObject> chatList = new List<GameObject>();

    public void OnChatMessage(string pChatter, string pMessage)
    {
        if (pMessage == "")
            return;

        if (pMessage.Length > 130)
            return;

        ClearChat();
        chatList.Add(Instantiate(ChatPrefab, ChatMessages.GetComponent<RectTransform>().position, Quaternion.identity, ChatMessages.transform));
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
        if (chatList.Count >= 8)
        {
            Destroy(chatList[0]);
            chatList.RemoveAt(0);
        }
    }

    private void Awake()
    {
        ConnectOnTwitch.ResetChatEventHandler += ConnectOnTwitch_ResetChatEventHandler;
    }

    private void ConnectOnTwitch_ResetChatEventHandler()
    {
        ClearOnRestart();
    }
}
