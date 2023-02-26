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

    public void MessagerTryConnect()
    {
        _messagerStatus = MessagerStatus.TryConnect;
        StartConnectTwitch();
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
    private void StartConnectTwitch()
    {
        if (_messagerStatus == MessagerStatus.Connected)
            return;

        Twitch = new TcpClient(URL, PORT);
        Reader = new StreamReader(Twitch.GetStream());
        Writer = new StreamWriter(Twitch.GetStream());

        //conexão da twitch
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
            return;

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
