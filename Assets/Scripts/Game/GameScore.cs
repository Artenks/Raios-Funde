using System;
using System.IO;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public GameManager GameManager;
    public event Action<int, int> UpdateScoreEventHandler;
    public struct ScoreData
    {
        public int Combo;
        public int Record;
    }
    public ScoreData Score;

    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/GameScore.json";

        if (File.Exists(_path))
        {
            var file = File.OpenText(_path);
            file.Close();
        }
        else
        {
            var file = File.CreateText(_path);
            file.Close();
        }
        Load();

        GameManager.GameScoreEventHandler += GameManager_GameScoreEventHandler;
    }

    private void GameManager_GameScoreEventHandler(bool win)
    {
        if (win)
        {
            Score.Combo++;
            if (Score.Combo > Score.Record)
                Score.Record = Score.Combo;
        }
        else
            Score.Combo = 0;

        Save();

        UpdateScoreEventHandler?.Invoke(Score.Combo, Score.Record);
    }

    private void Save()
    {
        var contentList = JsonUtility.ToJson(Score, true);

        File.WriteAllText(_path, contentList);
    }

    private void Load()
    {
        var contentPath = File.ReadAllText(_path);

        if (contentPath == "")
        {
            Save();
            return;
        }

        var content = JsonUtility.FromJson<ScoreData>(contentPath);

        Score.Record = content.Record;
        Score.Combo = content.Combo;

    }


}
