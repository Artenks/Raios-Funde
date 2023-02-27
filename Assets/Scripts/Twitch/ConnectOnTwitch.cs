using System;
using System.IO;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum TwitchStatus
{
    TryConnect,
    Connected,
    Disconnected,
}

[RequireComponent(typeof(ConnectTwitchMessager))]
public class ConnectOnTwitch : MonoBehaviour
{
    public TakeMessage TakeMessage;

    public event Action ResetChatEventHandler;
    public event Action<bool> InputChangeHandler;
    public event Action<bool> ConnectionHandler;

    public UnityEvent<string> TwitchTagsEvent;

    public TMP_InputField UserInput;
    public TMP_InputField OAuthInput;

    public TwitchStatus TwitchStatusVariable;

    private EmptyToNull _emptyToNull;
    private ConnectTwitchMessager _twitchMessager;

    private TcpClient Twitch;
    private StreamReader Reader;
    private StreamWriter Writer;

    private const string URL = "irc.chat.twitch.tv";
    private const int PORT = 6667;

    private string Channel = null;
    private string User = null;
    private string OAuth = null;

    private float PingCounter;

    private float _timeToReconnect = 5.0f;
    private float _realTime = 0;
    private bool _tryReconnect;

    private TwitchInfo _twitchInfo;
    private void TwitchTryConnect(bool noTakeMessage)
    {
        TwitchStatusVariable = TwitchStatus.TryConnect;
        StartConnectTwitch();
        _twitchMessager.TakeAMessage(User, OAuth, noTakeMessage);

    }
    private void TwitchReconnect()
    {
        TwitchDisconnect();

        TwitchTryConnect(true);
    }
    private void TwitchDisconnect()
    {
        TwitchStatusVariable = TwitchStatus.Disconnected;
        ConnectionHandler?.Invoke(false);

        if (Twitch != null)
        {
            Twitch.Close();
            Reader.Close();
            Writer.Close();

            Twitch = null;
            Reader = null;
            Writer = null;
        }

        _twitchMessager.MessagerDisconnect();

    }

    private void ReconnectInTime()
    {

        if (_realTime >= _timeToReconnect)
        {
            Debug.Log("Reconectando");
            TwitchReconnect();
            _realTime = 0;
            _tryReconnect = false;
        }
        else
        {
            _realTime += Time.deltaTime;
        }
    }

    private void StartConnectTwitch()
    {
        if (TwitchStatusVariable == TwitchStatus.Connected)
            return;

        Twitch = new TcpClient(URL, PORT);
        Reader = new StreamReader(Twitch.GetStream());
        Writer = new StreamWriter(Twitch.GetStream());

        Writer.WriteLine("PASS " + OAuth);
        Writer.WriteLine("NICK " + User);
        Writer.WriteLine("USER " + User + " 8 *:" + User);
        Writer.WriteLine("JOIN #" + Channel);
        Writer.WriteLine("CAP REQ :twitch.tv/commands twitch.tv/tags");
        Writer.WriteLine("PING" + URL);
        Writer.Flush();
    }

    private void SubmitInputUser(string msg)
    {
        var msgNoSpace = _emptyToNull.RemoveSpace(msg);

        if (msgNoSpace != User)
        {
            InputChangeHandler?.Invoke(true);

            _twitchInfo.SaveAInfo(true, msgNoSpace);
        }
        else if (msgNoSpace == User && TwitchStatusVariable == TwitchStatus.Connected)
        {
            InputChangeHandler?.Invoke(false);
        }
        ResetChatEventHandler?.Invoke();

        User = msgNoSpace;
        Channel = User;

        if (User != null && OAuth != null)
        {
            TwitchDisconnect();
            TwitchTryConnect(false);
            _tryReconnect = true;
        }
        else
        {
            TwitchDisconnect();
        }
    }
    private void SubmitInputOAuth(string msg)
    {
        var msgNoSpace = _emptyToNull.RemoveSpace(msg);

        if (msgNoSpace != OAuth)
        {
            InputChangeHandler?.Invoke(true);

            _twitchInfo.SaveAInfo(false, msgNoSpace);
        }
        else if (msgNoSpace == OAuth && TwitchStatusVariable == TwitchStatus.Connected)
        {
            InputChangeHandler?.Invoke(false);
        }
        ResetChatEventHandler?.Invoke();


        OAuth = msgNoSpace;

        if (User != null && OAuth != null)
        {
            TwitchDisconnect();
            TwitchTryConnect(false);
            _tryReconnect = true;
        }
        else
        {
            TwitchDisconnect();
        }
    }

    private void Awake()
    {
        _twitchMessager = GetComponent<ConnectTwitchMessager>();
        _twitchInfo = GameObject.Find("LoadTwitch").GetComponent<TwitchInfo>();

        TwitchDisconnect();
        _twitchMessager.MessagerDisconnect();
    }

    private void Start()
    {
        _emptyToNull = new EmptyToNull();

        _twitchInfo.LoginEventHandler += _twitchInfo_LoginEventHandler;

        UserInput.onSubmit.AddListener(SubmitInputUser);
        OAuthInput.onSubmit.AddListener(SubmitInputOAuth);

    }

    private void _twitchInfo_LoginEventHandler()
    {
        User = _emptyToNull.RemoveSpace(_twitchInfo.ContentJson.user);
        Channel = User;
        OAuth = _emptyToNull.RemoveSpace(_twitchInfo.ContentJson.oauth);

        if (User != null && OAuth != null)
        {
            UserInput.text = User;
            OAuthInput.text = OAuth;

            TwitchTryConnect(false);
        }
    }

    private void Update()
    {
        if (Twitch == null || !Twitch.Connected)
            return;

        if (_tryReconnect && TwitchStatusVariable == TwitchStatus.TryConnect)
        {
            ReconnectInTime();
        }

        if (PingCounter >= 20.0f && Twitch.Available == 0)
        {

            Writer.WriteLine("PING" + URL);
            Writer.Flush();

            PingCounter = 0;
        }
        else
        {
            PingCounter += Time.deltaTime;
        }

        if (Twitch.Available > 0)
        {
            var messageRead = Reader.ReadLine();
            messageRead = messageRead.Replace("\U000e0000", "");

            if (messageRead.Contains("PRIVMSG"))
            {
                if (TwitchStatusVariable == TwitchStatus.TryConnect)
                {
                    ConnectionHandler?.Invoke(true);

                    _tryReconnect = false;
                    _realTime = 0;

                    _twitchMessager.MessageRead = "OK";

                    TwitchStatusVariable = TwitchStatus.Connected;
                    _twitchMessager._messagerStatus = MessagerStatus.Connected;
                }
                TwitchTagsEvent?.Invoke(messageRead);
            }

        }

        if (_twitchMessager.MessageRead.Contains("CHECK") && TwitchStatusVariable == TwitchStatus.TryConnect)
        {
            TwitchStatusVariable = TwitchStatus.Connected;
            _twitchMessager._messagerStatus = MessagerStatus.Connected;

            ConnectionHandler?.Invoke(true);

            _tryReconnect = false;
            _realTime = 0;

            _twitchMessager.MessageRead = "OK";
        }

    }
}
