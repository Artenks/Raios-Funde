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
        discord = new Discord.Discord(1047329264137154612, (System.UInt64)CreateFlags.Default);
        activityManager = discord.GetActivityManager();

        menus();
    }

    void menus()
    {
        activity.Timestamps.Start = ToUnixTime();

        activity.Details = "Sofrendo em Raios Funde";
        activity.State = "Na tentativa de sobreviver ao maior terror de todos.";

        activity.Assets.LargeImage = "logo";
        activity.Assets.LargeText = "Raios Funde";

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Result.Ok)
            {
                Debug.Log("Discord: conectado");
            }
        });

        //timer
        long ToUnixTime()
        {
            DateTime date = DateTime.UtcNow;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }

    private void Update()
    {
        discord.RunCallbacks();
    }

    private void OnDisable()
    {
        discord.Dispose();
    }
}
