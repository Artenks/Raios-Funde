using UnityEngine;

public class InputChangeView : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;
    public TwitchChat TwitchChat;
    void Start()
    {
        ConnectOnTwitch = GameObject.FindObjectOfType<ConnectOnTwitch>();
        TwitchChat = FindObjectOfType<TwitchChat>();

        ConnectOnTwitch.InputChangeHandler += _connectOnTwitch_InputChangeEventHandler;
    }

    private void _connectOnTwitch_InputChangeEventHandler(bool parametersChanged)
    {
        if (parametersChanged)
            TwitchChat.OnChatMessage(".", "Lacuna atualizada.");
        else
            TwitchChat.OnChatMessage(".", "Informação recarregada.");

    }
}
