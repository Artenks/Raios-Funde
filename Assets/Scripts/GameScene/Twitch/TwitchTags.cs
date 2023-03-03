using System;
using UnityEngine;

public class TwitchTags : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;
    public TakeMessage TakeMessage;

    public ChatPass ChatPass;

    [Serializable]
    public struct TagsData
    {
        public string Badges;
        public string Color;
        public string DisplayName;
        public string Message;
        public bool Subscriber;
        public bool Mod;
        public bool Vip;
        public bool Broadcaster;
        public bool Pleb;
    }
    public TagsData Tags;

    [Serializable]
    public struct PassValues
    {
        public bool Everything;
        public bool Mod;
        public bool Vip;
        public bool Subscriber;
        public bool Pleb;
        public bool Broadcaster;
    }
    [SerializeField]
    private PassValues _passValues;

    public void TwitchTagsEvent(string badgeInfo)
    {
        if (badgeInfo == "")
            return;

        Tags = new TagsData();

        Tags.Badges = TakeATag(badgeInfo, "badges=");
        if (Tags.Badges.Contains("bot"))
            return;

        Tags.DisplayName = TakeATag(badgeInfo, "display-name=").ToLower();
        Tags.Message = TakeAMessage(badgeInfo);
        Tags.Color = TakeATag(badgeInfo, "color=");
        if (Tags.Color == "")
            Tags.Color = "#FFFFFF";

        Tags.Subscriber = int.Parse(TakeATag(badgeInfo, "subscriber=")) == 1 ? true : false;
        Tags.Pleb = int.Parse(TakeATag(badgeInfo, "subscriber=")) == 0 ? true : false;
        Tags.Mod = int.Parse(TakeATag(badgeInfo, "mod=")) == 1 ? true : false;
        Tags.Vip = Tags.Badges.Contains("vip/1") ? true : false;
        Tags.Broadcaster = Tags.Badges.Contains("broadcaster/1") ? true : false;

        if (_passValues.Everything)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
        else if (_passValues.Subscriber && Tags.Subscriber)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
        else if (_passValues.Pleb && Tags.Pleb)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
        else if (_passValues.Vip && Tags.Vip)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
        else if (_passValues.Mod && Tags.Mod)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);

        }
        else if (_passValues.Broadcaster && Tags.Broadcaster)
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
    }
    private string TakeATag(string badgeInfo, string target)
    {
        var textSplit = badgeInfo.Replace("@badge-info=", "").Split(';');
        var output = "";

        foreach (var line in textSplit)
        {
            if (line.Contains(target))
            {
                output = line.Substring(line.IndexOf('=') + 1);
            }
        }
        return output;
    }

    private string TakeAMessage(string fullMessage)
    {
        var msgIndex = fullMessage.IndexOf("PRIVMSG");
        var targetIndex = fullMessage.IndexOf(":", msgIndex);
        var message = fullMessage.Substring(targetIndex + 1);

        return message;
    }

    private void Awake()
    {
        _passValues.Everything = true;
        ChatPass.UpdatePassEventHandler += ChatPass_UpdatePassEventHandler;
    }

    private void ChatPass_UpdatePassEventHandler()
    {
        _passValues.Everything = ChatPass.Values.Everything.isOn;
        _passValues.Subscriber = ChatPass.Values.Subscriber.isOn;
        _passValues.Pleb = ChatPass.Values.Pleb.isOn;
        _passValues.Broadcaster = ChatPass.Values.Broadcast.isOn;
        _passValues.Vip = ChatPass.Values.Vip.isOn;
        _passValues.Mod = ChatPass.Values.Mod.isOn;
    }
}
