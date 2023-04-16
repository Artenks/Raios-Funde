using Discord;
using System;
using UnityEngine;

public class DiscordController : MonoBehaviour
{
    public static Discord.Discord discord;
    public ActivityManager activityManager;

    public Activity activity;
    private void Start()
    {
        discord = new Discord.Discord(1047329264137154612, (System.UInt64)CreateFlags.NoRequireDiscord);
        activityManager = discord.GetActivityManager();

        menus();
    }

    void menus()
    {
        activity.Timestamps.Start = ToUnixTime();

        activity.Details = "Sofrendo em Raios Funde";
        activity.State = "Na tentativa de sobreviver a esse pesadelo.";

        activity.Assets.LargeImage = "logo";
        activity.Assets.LargeText = "Raios Funde";

        //timer
        long ToUnixTime()
        {
            DateTime date = DateTime.UtcNow;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        activityManager.UpdateActivity(activity, (res) =>
        {
            switch (res)
            {
                case Result.Ok:
                    Debug.Log("Discord: conectado");
                    break;
                default:
                case Result.NotRunning:
                    Debug.Log("Discord: não está sendo rodado");
                    discord.Dispose();
                    return;
            }

        });
    }

    private void Update()
    {
        if (discord != null)
            discord.RunCallbacks();
    }

    private void OnDisable()
    {
        discord.Dispose();
    }
}
