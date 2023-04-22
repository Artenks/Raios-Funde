using System;
using UnityEngine;

public class TimeInGame : MonoBehaviour
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
        if (this.gameObject.activeInHierarchy)
        {
            if (!Data.RunTimer)
            {
                var realTime = timerTotal;
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
        return false;

    }
}
