using System;
using System.IO;
using TMPro;
using UnityEngine;

public class ConfigSave : MonoBehaviour
{
    public TMP_Text ConsoleDebug;

    public ResolutionScript ResolutionScript;
    public MusicManager MusicManager;
    public GameOnFirstExecute GameOnFirstExecute;

    private string _path;

    [Serializable]
    public struct ConfigInfo
    {
        public bool noIsFirstTime;

        [Serializable]
        public struct Screen
        {
            public int width;
            public int height;
            public float frameRate;
            public bool noIsFullscreen;
        }
        public Screen screen;

        [Serializable]
        public struct Audio
        {
            public bool noMusic;
            public float volume;
        }
        public Audio audio;
    }

    public ConfigInfo Info;

    void Awake()
    {
        _path = Application.persistentDataPath + "/GameConfigs.json";

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

        ResolutionScript.ResolutionEventHandler += ResolutionScript_ResolutionEventHandler;
        MusicManager.MusicEventHandler += MusicManager_MusicEventHandler;
        GameOnFirstExecute.FirstTimeEventHandler += GameOnFirstExecute_FirstTimeEventHandler;
    }

    private void GameOnFirstExecute_FirstTimeEventHandler()
    {
        Info.noIsFirstTime = true;
        Save();
    }

    private void MusicManager_MusicEventHandler()
    {
        Info.audio.volume = MusicManager.Info.volume;
        Info.audio.noMusic = !MusicManager.Info.music;
        Save();
    }

    private void ResolutionScript_ResolutionEventHandler()
    {
        Info.screen.frameRate = ResolutionScript.Info.frameRate;
        Info.screen.noIsFullscreen = !ResolutionScript.Info.isFullscreen;
        Info.screen.width = ResolutionScript.Info.width;
        Info.screen.height = ResolutionScript.Info.height;
        Save();
    }

    private void Save()
    {
        var content = JsonUtility.ToJson(Info, true);

        File.WriteAllText(_path, content);

        ConsoleDebug.text = content;

    }
    public void Load()
    {
        var contentPath = File.ReadAllText(_path);
        if (contentPath == "")
        {
            Save();
            return;
        }

        var content = JsonUtility.FromJson<ConfigInfo>(contentPath);

        Info.noIsFirstTime = content.noIsFirstTime;

        Info.screen.noIsFullscreen = content.screen.noIsFullscreen;
        Info.screen.frameRate = content.screen.frameRate;
        Info.screen.width = content.screen.width;
        Info.screen.height = content.screen.height;

        Info.audio.noMusic = content.audio.noMusic;
        Info.audio.volume = content.audio.volume;

        UpdateSecondaryInfo();
        Save();
    }

    private void UpdateSecondaryInfo()
    {
        ResolutionScript.Info.isFullscreen = !Info.screen.noIsFullscreen;
        ResolutionScript.Info.frameRate = Info.screen.frameRate;
        ResolutionScript.Info.width = Info.screen.width;
        ResolutionScript.Info.height = Info.screen.height;

        MusicManager.Info.music = !Info.audio.noMusic;
        MusicManager.Info.volume = Info.audio.volume;
    }

}
