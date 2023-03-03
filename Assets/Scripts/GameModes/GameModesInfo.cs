using System.IO;
using UnityEngine;

public class GameModesInfo : MonoBehaviour
{
    public struct GameModeInfo
    {
        public float Timer;
        public int TimerID;
        public int Tips;
        public int Chances;

    }
    public GameModeInfo Info;
    public GameDataUpdate GameDataUpdate;
    public GameManager Manager;
    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + "/GameModes.json";

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
    }

    public void CalculateInTimer()
    {
        if (Info.Timer > 60 && GameDataUpdate.CalculateTimer)
        {
            Info.Timer = Info.Timer - 40;
            GameDataUpdate.CalculateTimer = false;
        }
    }

    private void Save()
    {
        CalculateInTimer();
        var contentList = JsonUtility.ToJson(Info, true);

        File.WriteAllText(_path, contentList);

        Manager.ConfigurateGameModes();
    }

    private void Load()
    {
        var contentPath = File.ReadAllText(_path);

        if (contentPath == "")
        {
            Save();
            return;
        }

        var content = JsonUtility.FromJson<GameModeInfo>(contentPath);

        Info.Timer = content.Timer;
        Info.TimerID = content.TimerID;
        Info.Chances = content.Chances;
        Info.Tips = content.Tips;
    }

    public void GameStats(string type, float ItemNumber)
    {
        switch (type)
        {
            case "timer":
                Info.TimerID = (int)ItemNumber;
                break;
            case "tips":
                Info.Tips = (int)ItemNumber;
                break;
            case "chances":
                Info.Chances = (int)ItemNumber;
                break;
        }
        Save();
    }
}
