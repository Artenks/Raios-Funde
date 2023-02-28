using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameDataUpdate : MonoBehaviour
{
    public enum GameStatus
    {
        timer,
        tips,
        chances,
    }
    public GameStatus Stats;
    public UnityEvent<string, float> GameStats;
    public GameModesInfo Modes;

    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        UpdateGameModes();
    }

    private void UpdateGameModes()
    {
        switch (Stats)
        {
            case GameStatus.timer:
                Modes.Info.Timer = TimerToFloat(dropdown.captionText.text);
                dropdown.value = Modes.Info.TimerID;
                break;
            case GameStatus.tips:
                dropdown.value = Modes.Info.Tips;
                break;
            case GameStatus.chances:
                dropdown.value = Modes.Info.Chances;
                break;
        }
    }
    public void DataChangeValue()
    {
        switch (Stats)
        {
            case GameStatus.timer:
                Modes.Info.Timer = TimerToFloat(dropdown.captionText.text);
                GameStats?.Invoke("timer", dropdown.value);
                break;
            case GameStatus.tips:
                GameStats?.Invoke("tips", dropdown.value);
                break;
            case GameStatus.chances:
                GameStats?.Invoke("chances", dropdown.value);
                break;
        }
    }

    private float TimerToFloat(string valueInString)
    {
        if (dropdown.value != 0)
        {
            string output = "";
            for (var i = 0; i <= valueInString.Length - 1; i++)
            {
                if (valueInString[i] == ':')
                {
                    output += '.';
                    continue;
                }

                output += valueInString[i];
            }
            return float.Parse(output);
        }

        return 0;

    }
}