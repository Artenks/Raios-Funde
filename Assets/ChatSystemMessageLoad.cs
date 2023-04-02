using TMPro;
using UnityEngine;

public class ChatSystemMessageLoad : MonoBehaviour
{
    public TMP_Text MessageText;
    private TwitchChat _twitchChat;

    void Start()
    {
        _twitchChat = FindObjectOfType<TwitchChat>();
        MessageText.text = _twitchChat.SystemMessage;
    }
}
