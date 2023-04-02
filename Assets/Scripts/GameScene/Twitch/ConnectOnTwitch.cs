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

    //public event Action ResetChatEventHandler;
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

    private float _timeToReconnect = 1.0f;
    private float _realTime = 0;

    private TwitchInfo _twitchInfo;

    private void TwitchTryConnect(bool noTakeMessage)
    {
        TwitchStatusVariable = TwitchStatus.TryConnect;
        StartImediateTwitch();
        _twitchMessager.TakeAMessage(User, OAuth, noTakeMessage);
    }
    private void TwitchReconnect()
    {
        TwitchTryConnect(true);
    }
    private void TwitchDisconnect()
    {
        TwitchStatusVariable = TwitchStatus.Disconnected;
        ConnectionHandler?.Invoke(false);

        _twitchMessager.MessagerDisconnect();
        if (Twitch != null)
        {
            Twitch.Close();
            Reader.Close();
            Writer.Close();

            Twitch = null;
            Reader = null;
            Writer = null;
        }
    }

    private void ReconnectInTime()
    {
        if (_realTime >= _timeToReconnect)
        {
            TwitchReconnect();
        }
        else
        {
            _realTime += Time.deltaTime;
        }
    }

    private void StartImediateTwitch()
    {
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
        if (msg.Contains("\n") || msg.Contains("\r"))
            return;

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

        User = msgNoSpace;
        Channel = User;

        if (User != null && OAuth != null)
        {
            TwitchReconnect();
        }
        else
        {
            TwitchDisconnect();
        }
    }
    private void SubmitInputOAuth(string msg)
    {
        if (msg.Contains("\n") || msg.Contains("\r"))
            return;

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


        OAuth = msgNoSpace;

        if (User != null && OAuth != null)
        {
            TwitchReconnect();

        }
        else
        {
            TwitchDisconnect();
        }
    }

    private void Awake()
    {
        _twitchMessager = GetComponent<ConnectTwitchMessager>();
        _twitchInfo = GameObject.Find("Load").GetComponent<TwitchInfo>();

        TwitchReconnect();
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

        if (User != null || OAuth != null)
        {
            UserInput.text = User;
            OAuthInput.text = OAuth;

            TwitchTryConnect(false);
        }
    }

    private void Update()
    {
        if (Twitch == null || !Twitch.Connected)
        {
            if (User != null && OAuth != null)
            {
                PingCounter = 0;
                ReconnectInTime();

                ConnectionHandler?.Invoke(false);
            }
            return;
        }

        if (PingCounter >= 5.0f && Twitch.Available == 0)
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

                    _twitchMessager.MessageRead = "OK";

                    TwitchStatusVariable = TwitchStatus.Connected;
                    _twitchMessager._messagerStatus = MessagerStatus.Connected;
                }
                TwitchTagsEvent?.Invoke(messageRead);
            }
        }


        if (_twitchMessager.MessageRead.Contains("CHECK") && TwitchStatusVariable == TwitchStatus.TryConnect)
        {
            _realTime = 0;

            TwitchStatusVariable = TwitchStatus.Connected;
            _twitchMessager._messagerStatus = MessagerStatus.Connected;

            ConnectionHandler?.Invoke(true);

            _twitchMessager.MessageRead = "OK";
        }

    }
}
