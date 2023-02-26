using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatPass : MonoBehaviour
{
    public event Action UpdatePassEventHandler;

    public TwitchTags TwitchTags;

    [Serializable]
    public struct Pass
    {
        public Toggle Everything;
        public Toggle Subscriber;
        public Toggle Pleb;
        public Toggle Vip;
        public Toggle Mod;
        public Toggle Broadcast;
    }
    public Pass Values;

    public void ValueChanged(string toggleName, Toggle toggle)
    {
        switch (toggleName)
        {
            case "everything":
                Values.Everything = toggle;

                if (Values.Everything.isOn)
                {
                    if (Values.Subscriber)
                        Values.Subscriber.isOn = false;

                    if (Values.Pleb)
                        Values.Pleb.isOn = false;

                    if (Values.Vip)
                        Values.Vip.isOn = false;

                    if (Values.Mod)
                        Values.Mod.isOn = false;

                    if (Values.Broadcast)
                        Values.Broadcast.isOn = false;

                }
                UpdatePassEventHandler?.Invoke();
                break;
            case "subscribers":
                Values.Subscriber = toggle;

                if (Values.Subscriber.isOn)
                {
                    if (Values.Everything.isOn)
                        Values.Everything.isOn = false;
                }
                UpdatePassEventHandler?.Invoke();
                break;
            case "plebs":
                Values.Pleb = toggle;

                if (Values.Pleb.isOn)
                {
                    if (Values.Everything.isOn)
                        Values.Everything.isOn = false;
                }
                UpdatePassEventHandler?.Invoke();
                break;
            case "vips":
                Values.Vip = toggle;

                if (Values.Vip.isOn)
                {
                    if (Values.Everything.isOn)
                        Values.Everything.isOn = false;
                }
                UpdatePassEventHandler?.Invoke();
                break;
            case "mods":
                Values.Mod = toggle;

                if (Values.Mod.isOn)
                {
                    if (Values.Everything.isOn)
                        Values.Everything.isOn = false;
                }
                UpdatePassEventHandler?.Invoke();
                break;
            case "broadcaster":
                Values.Broadcast = toggle;

                if (Values.Broadcast.isOn)
                {
                    if (Values.Everything.isOn)
                        Values.Everything.isOn = false;
                }
                UpdatePassEventHandler?.Invoke();
                break;
        }


    }

    //bool
    public bool UserPassToChat(bool mod, bool vip, bool sub, bool pleb, bool broadcast)
    {
        if (!Values.Everything)
            return true;

        if (!Values.Everything.isOn)
        {
            if (Values.Broadcast.isOn)
            {
                if (broadcast)
                    return true;
            }

            if (Values.Mod.isOn)
            {
                if (mod)
                    return true;
            }

            if (Values.Vip.isOn)
            {
                if (vip)
                    return true;
            }

            if (Values.Subscriber.isOn)
            {
                if (sub)
                    return true;
            }

            if (Values.Pleb.isOn)
            {
                if (pleb)
                    return true;
            }
        }
        else
        {
            return true;
        }

        return false;
    }
}
