using System;
using System.IO;
using TMPro;
using UnityEngine;
using Application = UnityEngine.Application;

public class TwitchInfo : MonoBehaviour
{
    public event Action LoginEventHandler;

    [Serializable]
    public struct Info
    {
        public bool isModified;
        public string user;
        public string oauth;
    }
    [SerializeField]
    public Info ContentJson;

    public TMP_Text ConsoleDebug;

    private string _path;

    public void SaveAInfo(bool isUser, string msg)
    {
        if (isUser)
        {
            ContentJson.user = msg;
        }
        else
        {
            ContentJson.oauth = msg;
        }

        if (!ContentJson.isModified)
            ContentJson.isModified = true;

        SaveJson();
    }

    public void LoadJson()
    {
        var content = File.ReadAllText(_path);

        if (content == "")
        {
            SaveJson();
            return;
        }

        var contentJson = JsonUtility.FromJson<Info>(content);

        ContentJson.isModified = contentJson.isModified;
        ContentJson.user = contentJson.user;
        ContentJson.oauth = contentJson.oauth;

        ConsoleDebug.text = content;
    }

    private void SaveJson()
    {
        var contentToSave = JsonUtility.ToJson(ContentJson, true);

        File.WriteAllText(_path, contentToSave);
        ConsoleDebug.text = contentToSave;
    }

    private void Start()
    {
        _path = Application.persistentDataPath + "/TwitchLogin.json";

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

        LoadJson();
        LoginEventHandler?.Invoke();
    }

}
