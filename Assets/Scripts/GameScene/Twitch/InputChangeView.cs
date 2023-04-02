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

    private void _connectOnTwitch_InputChangeEventHandler(bool parametersChanged)
    {
        if (parametersChanged)
            _twitchChat.OnChatSystemMessage("Lacuna atualizada.");
        else
            _twitchChat.OnChatSystemMessage("Informação recarregada.");

    }


}
