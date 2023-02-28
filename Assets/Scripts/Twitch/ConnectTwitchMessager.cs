using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

[Serializable]
public enum MessagerStatus
{
    TryConnect,
    Connected,
    Disconnected,
}
[RequireComponent(typeof(ConnectOnTwitch))]
public class ConnectTwitchMessager : MonoBehaviour
{
    public MessagerStatus _messagerStatus;

    public string MessageRead;


    private TcpClient Twitch;
    private StreamReader Reader;
    private StreamWriter Writer;

    private const string URL = "irc.chat.twitch.tv";
    private const int PORT = 6667;

    private string Channel = null;
    private string User = null;
    private string OAuth = null;

    private bool _messageGo = false;

    private float PingCounter;

    private float _timeToReconnect = 1.0f;
    private float _realTime = 0;

    public void MessagerTryConnect()
    {
        _messagerStatus = MessagerStatus.TryConnect;
        StartImediateMessager();
    }
    private void TwitchReconnect()
    {
        StartImediateMessager();
    }
    public void MessagerDisconnect()
    {
        _messagerStatus = MessagerStatus.Disconnected;

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

    public void TakeAMessage(string user, string oAuth, bool noTakeMessage)
    {
        _messageGo = noTakeMessage;

        User = user;
        Channel = User;
        OAuth = oAuth;

        MessagerTryConnect();
    }

    //para ChatMessage
    public void SendAMessage(string messageSend)
    {
        if (User != null)
        {
            Writer.WriteLine($"PRIVMSG #{User} : {messageSend}");
            Writer.Flush();
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

    private void StartImediateMessager()
    {
        Twitch = new TcpClient(URL, PORT);
        Reader = new StreamReader(Twitch.GetStream());
        Writer = new StreamWriter(Twitch.GetStream());

        Writer.WriteLine("PASS " + OAuth);
        Writer.WriteLine("NICK " + User);
        Writer.WriteLine("USER " + User + " 8 *:" + User);
        Writer.WriteLine("JOIN #" + Channel);
        Writer.WriteLine("PING" + URL);
        Writer.Flush();
    }
    private void WriteToChannel(string channelName, string messageSend)
    {
        if (!_messageGo)
        {
            //Writer.WriteLine($"PRIVMSG #{channelName} : {messageSend}");

        }
        Writer.WriteLine("CHECK");
        Writer.Flush();
    }

    private void Awake()
    {
        MessageRead = "";
    }

    private void Update()
    {
        if (Twitch == null || !Twitch.Connected)
        {
            if (User != null && OAuth != null)
            {
                PingCounter = 0;
                ReconnectInTime();
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

            if (messageRead.Contains("CHECK"))
            {
                MessageRead = messageRead;
            }

            if (_messagerStatus == MessagerStatus.TryConnect)
            {
                StartCoroutine(WriteInChannel());
            }
        }

    }

    private IEnumerator WriteInChannel()
    {
        WriteToChannel(Channel, "Raios Funde conectou chefia BroBalt ");
        _messageGo = true;
        yield return new WaitForEndOfFrame();

    }

}
