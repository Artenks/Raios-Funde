using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class RankInfo : MonoBehaviour
{
    public UnityEvent<List<string>> RankUpdaterEvent;

    [Serializable]
    public struct RankData
    {
        public List<string> UsersInfo;
    }
    public RankData Data;

    private string _path;
    private void Awake()
    {
        _path = Application.persistentDataPath + "/Rank.json";

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

    public void Save()
    {
        var contentPath = JsonUtility.ToJson(Data, true);
        File.WriteAllText(_path, contentPath);

        RankUpdaterEvent?.Invoke(Data.UsersInfo);
    }

    public void Load()
    {
        var contentPath = File.ReadAllText(_path);

        if (contentPath == "")
        {
            Save();
            return;
        }

        var content = JsonUtility.FromJson<RankData>(contentPath);

        Data.UsersInfo = content.UsersInfo;

        RankUpdaterEvent?.Invoke(Data.UsersInfo);
    }

    public string TakeAUser(string userInfo)
    {
        var userOutput = "";
        foreach (var item in Data.UsersInfo)
        {
            var output = item.Substring(0, item.IndexOf(','));

            if (userInfo.Contains(output))
            {
                userOutput = output;
                return userOutput;
            }
        }

        return userOutput;

    }

    public bool UserExist(string user)
    {
        if (TakeAUser(user) == user)
        {
            return true;

        }
        return false;
    }

    public int TakeAScoreUser(string user)
    {
        int output = 0;
        foreach (var item in Data.UsersInfo)
        {
            if (item.Contains(user))
            {
                output = int.Parse(item.Substring(item.IndexOf(',') + 1));
                return output;
            }
        }
        return output;
    }

    public void AddAUser(string user, int score)
    {
        Data.UsersInfo.Add($"{user},{score}");
    }

    public void UpdateUserScore(string user, int addInScore)
    {
        var userIndex = 0;
        bool valueChange = false;
        foreach (var item in Data.UsersInfo)
        {
            if (TakeAUser(user) == user)
            {
                var userName = item.Substring(0, item.IndexOf(','));
                if (userName == user)
                {
                    userIndex = Data.UsersInfo.IndexOf(item);
                    valueChange = true;
                    break;
                }
            }
        }
        if (valueChange)
            Data.UsersInfo[userIndex] = $"{user},{TakeAScoreUser(user) + addInScore}";
    }
}
