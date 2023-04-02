using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreView : MonoBehaviour
{
    public event Action<int, int> StreakScoreEventHandler;

    public TMP_Text ScoreStreak;
    public TMP_Text ScoreRecord;

    private GameScore _gameScore;
    void Awake()
    {
        _gameScore = GetComponent<GameScore>();
        _gameScore.UpdateScoreEventHandler += _gameScore_UpdateScoreEventHandler;

        _gameScore_UpdateScoreEventHandler(_gameScore.Score.Combo, _gameScore.Score.Record);
    }

    private void _gameScore_UpdateScoreEventHandler(int streak, int record)
    {
        if (streak > 0)
        {
            ScoreStreak.GetComponentInParent<Image>(true).gameObject.SetActive(true);
            ScoreStreak.text = $"{streak}";
        }
        else
            ScoreStreak.GetComponentInParent<Image>(true).gameObject.SetActive(false);

        if (record > 0)
        {
            ScoreRecord.GetComponentInParent<Image>(true).gameObject.SetActive(true);
            ScoreRecord.text = $"{record}";
        }
        else
            ScoreRecord.GetComponentInParent<Image>(true).gameObject.SetActive(false);

        StreakScoreEventHandler?.Invoke(streak, record);
    }

}
