using UnityEngine;

public class InputChangeView : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;
    private TwitchChat _twitchChat;
    void Start()
    {
        ConnectOnTwitch = GameObject.FindObjectOfType<ConnectOnTwitch>();
        _twitchChat = FindObjectOfType<TwitchChat>();

        ConnectOnTwitch.InputChangeHandler += _connectOnTwitch_InputChangeEventHandler;
    }

    private void _connectOnTwitch_InputChangeEventHandler()
    {
        _twitchChat.OnChatSystemMessage("Canal atualizado");
    }


}
