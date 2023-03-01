using System;
using UnityEngine;
using UnityEngine.UI;

public class TwitchDictionary : MonoBehaviour
{
    public event Action<bool> TwitchDictionaryStatusEventHandler;

    public Toggle TwitchToggle;
    public ConfigSave ConfigSave;
    public void UpdateStatus()
    {
        TwitchDictionaryStatusEventHandler?.Invoke(this.GetComponent<Toggle>().isOn);
    }
}
