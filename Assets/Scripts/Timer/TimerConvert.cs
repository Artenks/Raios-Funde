using System;
using UnityEngine;

public class TimerConvert : MonoBehaviour
{
    [Serializable]
    public struct TimerData
    {
        public float Minutes;
        public float Seconds;
    }
    public TimerData Data;

    public string TimerConverted(float timer)
    {
        var realTimer = timer;

        Data.Minutes = (Mathf.Floor(realTimer / 60));
        Data.Seconds = (realTimer % 60);

        if (Data.Minutes <= 0 && Data.Seconds > 0)
        {
            return Data.Seconds.ToString("00.00");
        }

        return Data.Minutes.ToString("00") + ":" + Data.Seconds.ToString("00");
    }
}
