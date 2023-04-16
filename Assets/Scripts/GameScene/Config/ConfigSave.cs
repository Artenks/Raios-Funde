using System;
using System.IO;
using UnityEngine;

public class ConfigSave : MonoBehaviour
{
    public ResolutionScript ResolutionScript;
    public MusicManager MusicManager;
    public EffectsManager EffectsManager;
    public GameOnFirstExecute GameOnFirstExecute;

    private string _path;

    [Serializable]
    public struct ConfigInfo
    {
        public bool noIsFirstTimePlaying;

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
            public float musicVolume;
            public bool noSoundEffects;
            public float effectsVolume;
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
        EffectsManager.EffectsEventHandler += EffectsManager_EffectsEventHandler;
    }

    public void ChangeFirstTime()
    {
        Info.noIsFirstTimePlaying = true;
        Save();
    }

    private void MusicManager_MusicEventHandler()
    {
        Info.audio.musicVolume = MusicManager.Info.volume;
        Info.audio.noMusic = !MusicManager.Info.music;
        Save();
    }
    private void EffectsManager_EffectsEventHandler()
    {
        Info.audio.effectsVolume = EffectsManager.Info.volume;
        Info.audio.noSoundEffects = !EffectsManager.Info.effects;
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

        Info.noIsFirstTimePlaying = content.noIsFirstTimePlaying;

        Info.screen.noIsFullscreen = content.screen.noIsFullscreen;
        Info.screen.frameRate = content.screen.frameRate;
        Info.screen.width = content.screen.width;
        Info.screen.height = content.screen.height;

        Info.audio.noMusic = content.audio.noMusic;
        Info.audio.musicVolume = content.audio.musicVolume;

        Info.audio.noSoundEffects = content.audio.noSoundEffects;
        Info.audio.effectsVolume = content.audio.effectsVolume;

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
        MusicManager.Info.volume = Info.audio.musicVolume;

        EffectsManager.Info.effects = !Info.audio.noSoundEffects;
        EffectsManager.Info.volume = Info.audio.effectsVolume;
    }

}
