using Lexone.UnityTwitchChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum TwitchStatus
{
    Connected,
    Disconnected,
}

public class ConnectOnTwitch : MonoBehaviour
{
    public IRC TwitchIRC;

    public TakeMessage TakeMessage;

    public event Action InputChangeHandler;
    public event Action<bool> ConnectionHandler;
    public event Action ChatListEventHandler;

    public UnityEvent<Chatter> TwitchTagsEvent;

    public TMP_InputField UserInput;

    public TwitchStatus TwitchStatusVariable;

    private EmptyToNull _emptyToNull;

    private string User = null;

    private float PingCounter;
    private TwitchInfo _twitchInfo;

    public void TwitchDisconnect()
    {
        TwitchIRC.LeaveChannel(TwitchIRC.channel);
        TwitchIRC.Disconnect();
        TwitchStatusVariable = TwitchStatus.Disconnected;
        ConnectionHandler?.Invoke(false);
    }
    private void StartImediateTwitch()
    {
        if (User == null)
            return;

        ChatListEventHandler?.Invoke();

        //TwitchIRC.channel = User;
        if (TwitchStatusVariable == TwitchStatus.Disconnected)
        {
            TwitchIRC.channel = User;
            TwitchIRC.Connect();
        }
        else
        {
            TwitchIRC.LeaveChannel(TwitchIRC.channel);
            TwitchIRC.JoinChannel(User);
        }

    }

    public void SubmitInputUser(string msg)
    {
        if (msg.Contains("\n") || msg.Contains("\r"))
            return;

        var msgNoSpace = _emptyToNull.RemoveSpace(msg);

        if (msgNoSpace != User)
        {
            InputChangeHandler?.Invoke();

            _twitchInfo.SaveAInfo(true, msgNoSpace);
        }

        User = msgNoSpace;

        if (User != null)
        {
            StartImediateTwitch();
        }
        else
        {
            TwitchDisconnect();
        }
    }

    private void Awake()
    {
        TwitchStatusVariable = TwitchStatus.Disconnected;

        TwitchIRC.OnConnectionAlert += TwitchIRC_OnConnectionAlert;
        TwitchIRC.OnChatMessage += TwitchIRC_OnChatMessage;

        _twitchInfo = GameObject.Find("Informations").GetComponent<TwitchInfo>();
    }

    private void TwitchIRC_OnConnectionAlert(IRCReply alert)
    {
        switch (alert)
        {
            case IRCReply.BAD_LOGIN:
            case IRCReply.NO_CONNECTION:
            case IRCReply.MISSING_LOGIN_INFO:
            default:
                TwitchStatusVariable = TwitchStatus.Disconnected;
                ConnectionHandler?.Invoke(false);
                break;
            case IRCReply.JOINED_CHANNEL:
                TwitchStatusVariable = TwitchStatus.Connected;
                ConnectionHandler?.Invoke(true);
                break;
        }
    }

    private void TwitchIRC_OnChatMessage(Chatter chatter)
    {
        TwitchTagsEvent?.Invoke(chatter);
    }

    private void Start()
    {
        _emptyToNull = new EmptyToNull();
        UserInput.onSubmit.AddListener(SubmitInputUser);
        _twitchInfo.LoginEventHandler += _twitchInfo_LoginEventHandler;

        StartImediateTwitch();
    }

    private void _twitchInfo_LoginEventHandler()
    {
        User = _emptyToNull.RemoveSpace(_twitchInfo.ContentJson.user);

        if (User != null)
        {
            UserInput.text = User;

            StartImediateTwitch();
        }
    }

    private void Update()
    {

        if (PingCounter >= 3.0f && TwitchStatusVariable == TwitchStatus.Disconnected)
        {
            StartImediateTwitch();
            PingCounter = 0;
        }
        else
        {
            PingCounter += Time.deltaTime;
        }

    }
}
