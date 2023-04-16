using Lexone.UnityTwitchChat;
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

    public void TwitchTagsEvent(Chatter chatter)
    {
        if (chatter.message == "")
            return;

        Tags = new TagsData();

        Tags.DisplayName = chatter.tags.displayName;
        Tags.Message = chatter.message;
        Tags.Color = chatter.tags.colorHex;
        if (Tags.Color == "")
            Tags.Color = "#FFFFFF";

        Tags.Subscriber = chatter.HasBadge("subscriber");
        Tags.Pleb = chatter.HasBadge("subscriber") == true ? false : true;
        Tags.Mod = chatter.HasBadge("moderator");
        Tags.Vip = chatter.HasBadge("vip");
        Tags.Broadcaster = chatter.HasBadge("broadcaster");

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
        else if (Tags.DisplayName == ".")
        {
            TakeMessage.SendAMessage(Tags.DisplayName, Tags.Message);
        }
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
