using System;
using TMPro;
using UnityEngine;

public class GameScoreView : MonoBehaviour
{
    public event Action<int, int> StreakScoreEventHandler;

    public TMP_Text ScoreCombo;
    public TMP_Text ScoreRecord;

    private GameScore _gameScore;
    void Awake()
    {
        _gameScore = GetComponent<GameScore>();
        _gameScore.UpdateScoreEventHandler += _gameScore_UpdateScoreEventHandler;

        _gameScore_UpdateScoreEventHandler(_gameScore.Score.Combo, _gameScore.Score.Record);
    }

    private void _gameScore_UpdateScoreEventHandler(int combo, int record)
    {
        if (combo > 0)
        {
            ScoreCombo.gameObject.SetActive(true);
            ScoreCombo.text = $"Combo: {combo}";
        }
        else
            ScoreCombo.gameObject.SetActive(false);

        if (record > 0)
        {
            ScoreRecord.gameObject.SetActive(true);
            ScoreRecord.text = $"Record: {record}";
        }
        else
            ScoreRecord.gameObject.SetActive(false);

        StreakScoreEventHandler?.Invoke(combo, record);
    }

}
