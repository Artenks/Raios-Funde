using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessageLoad : MonoBehaviour
{
    public event Action<string> PortraitEventHandler;

    public TMP_Text UserText;
    public TMP_Text MessageText;

    public ChatBoxColor ChatBoxColor;
    public CaractereRemove CaractereRemove;

    public TwitchChat TwitchChat;
    public GameObject Badges;


    private TwitchTags _twitchTags;

    public bool TakeAPortrait;
    void Start()
    {
        TakeAPortrait = true;

        _twitchTags = GameObject.FindGameObjectWithTag("Connection").GetComponent<TwitchTags>();
        TwitchChat = FindObjectOfType<TwitchChat>();

        UserText.color = HexColorToRGB(TwitchChat.RecentUserInfo.Color);
        UserText.text = TwitchChat.RecentUserInfo.DisplayName + ":";
        MessageText.text = TwitchChat.RecentUserInfo.Message;
        MessageText.ignoreVisibility = true;

        //UserText.text = TwitchChat.UsersInfo[TwitchChat.Index].DisplayName + ":";
        //MessageText.text = TwitchChat.UsersInfo[TwitchChat.Index].Message;
        //MessageText.ignoreVisibility = true;

        UserHaveBadges();
        ChoiceUserPortrait();

        ChatBoxColor.ChangeBoxColor(CaractereRemove.RemoveDiacritics(TwitchChat.RecentUserInfo.Message));

        TwitchChat.UsersInfo.Remove(TwitchChat.UsersInfo[0]);
        TwitchChat.SendNewChatMessage = true;
    }

    private Color HexColorToRGB(string hex)
    {
        Color rgbColor;
        ColorUtility.TryParseHtmlString(hex, out rgbColor);

        return rgbColor;
    }

    private void UserHaveBadges()
    {
        var badges = Badges.GetComponentsInChildren<Image>(true);

        foreach (var badge in badges)
        {
            if (badge.gameObject.name == "Broadcaster" && _twitchTags.Tags.Broadcaster)
            {
                badge.gameObject.SetActive(true);
                continue;
            }
            else if (badge.gameObject.name == "Broadcaster")
                badge.gameObject.SetActive(false);


            if (badge.gameObject.name == "Mod" && _twitchTags.Tags.Mod)
            {
                badge.gameObject.SetActive(true);
                continue;
            }
            else if (badge.gameObject.name == "Mod")
                badge.gameObject.SetActive(false);

            if (badge.gameObject.name == "Vip" && _twitchTags.Tags.Vip)
            {
                badge.gameObject.SetActive(true);
                continue;
            }
            else if (badge.gameObject.name == "Vip")
                badge.gameObject.SetActive(false);
        }
    }

    private void ChoiceUserPortrait()
    {
        if (!TakeAPortrait)
            return;

        else if (_twitchTags.Tags.Broadcaster)
        {
            if (_twitchTags.Tags.DisplayName.ToLower() == "cellbit")
                PortraitInvoke("BroadcasterC");
            else
                PortraitInvoke("Broadcaster");
        }
        else if (_twitchTags.Tags.Mod)
            PortraitInvoke("Mod");

        else if (_twitchTags.Tags.Vip)
            PortraitInvoke("Vip");

        else if (_twitchTags.Tags.Subscriber)
            PortraitInvoke("Subscriber");

        else if (_twitchTags.Tags.Pleb)
            PortraitInvoke("Pleb");
    }

    private void PortraitInvoke(string portrait)
    {
        PortraitEventHandler?.Invoke(portrait);
        TakeAPortrait = false;
    }
}
