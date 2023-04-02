using TMPro;
using UnityEngine;


public class MultilinesRemove : MonoBehaviour
{
    public TMP_InputField InputField;
    public TwitchChat _twitchChat;

    private void Start()
    {
        InputField.onDeselect.AddListener(HaveMultilines);
        InputField.onSubmit.AddListener(HaveMultilines);
    }

    private void HaveMultilines(string msg)
    {
        MultilinesNoPermit(msg);
    }
    public void MultilinesNoPermit(string msg)
    {

        if (msg.Contains("\n") || msg.Contains("\r"))
        {
            _twitchChat.OnChatSystemMessage("A lacuna não pode conter mais de uma linha.");
            InputField.text = "";
        }
    }
}