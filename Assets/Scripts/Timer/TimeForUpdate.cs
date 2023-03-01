using System;
using UnityEngine;

public class TimeForUpdate : MonoBehaviour
{
    [Serializable]
    public struct TimerData
    {
        public float NowTimer;
        public float TotalTimer;
        public bool RunTimer;

    }
    public TimerData Data;

    public bool StartTheCount(float timerTotal)
    {
        if (!Data.RunTimer)
        {
            var realTime = timerTotal;
            if (timerTotal > 60)
                realTime = realTime - 40;
            Data.TotalTimer = realTime;
            Data.NowTimer = Data.TotalTimer;
            Data.RunTimer = true;
        }

        if (Data.NowTimer <= 0)
        {
            Data.TotalTimer = 0;
            Data.RunTimer = false;
            Data.NowTimer = 0;
            return true;
        }
        else
        {
            Data.NowTimer -= Time.deltaTime;
            return false;
        }
    }
}
