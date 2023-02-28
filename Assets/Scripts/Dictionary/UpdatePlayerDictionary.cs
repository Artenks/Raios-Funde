using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UpdatePlayerDictionary : MonoBehaviour
{
    public UnityEvent<string> PhraseFromDictionary;

    [Serializable]
    public struct AllPhrases
    {
        public int totalPhrase;
        public List<string> phraseList;
    }
    public AllPhrases ListDictionary;
    public TextAsset Dictionary;
    public GameManager GameManager;

    private string _pathUserDictionary;
    private void Awake()
    {
        _pathUserDictionary = Application.persistentDataPath + "/PlayerDictionary.json";

        if (File.Exists(_pathUserDictionary))
        {
            var file = File.OpenText(_pathUserDictionary);
            file.Close();
        }
        else
        {
            var file = File.CreateText(_pathUserDictionary);
            file.Close();
        }

        GameManager.ReceiveAPhraseEventHandler += GameManager_ReceiveAPhraseEventHandler;
    }

    private void GameManager_ReceiveAPhraseEventHandler()
    {
        PickPhrase();
    }

    private void Start()
    {
        Load();
        UpdateDictionary();
    }

    private void UpdateDictionary()
    {
        if (ListDictionary.totalPhrase < 10)
        {
            var content = Dictionary.text.Split();
            do
            {
                var randomPhraseRank = Random.Range(1, content.Length - 1);
                if (content[randomPhraseRank].Length == 5 && ListDictionary.totalPhrase < 10)
                {
                    var phrase = content[randomPhraseRank];
                    var rightPhrase = "";
                    for (int i = 0; i <= phrase.Length - 1; i++)
                    {
                        if (i == 0)
                        {
                            rightPhrase += phrase[i].ToString().ToUpper();
                            continue;
                        }
                        rightPhrase += phrase[i];
                    }

                    ListDictionary.phraseList.Add(rightPhrase);
                    ListDictionary.totalPhrase = ListDictionary.phraseList.Count;
                }
            }
            while (ListDictionary.totalPhrase < 10);

            Save();
        }
    }

    public void PickPhrase()
    {
        var randomNumber = Random.Range(0, ListDictionary.totalPhrase);
        var phrase = ListDictionary.phraseList[randomNumber];
        RemoveFromUserDictionary(phrase);

        PhraseFromDictionary?.Invoke(phrase);
    }

    public void RemoveFromUserDictionary(string phrase)
    {
        ListDictionary.phraseList.Remove(phrase);

        Save();
    }

    private void Save()
    {
        ListDictionary.totalPhrase = ListDictionary.phraseList.Count;

        var contentList = JsonUtility.ToJson(ListDictionary, true);

        File.WriteAllText(_pathUserDictionary, contentList);
    }

    private void Load()
    {
        var contentPath = File.ReadAllText(_pathUserDictionary);

        if (contentPath == "")
        {
            Save();
            return;
        }

        var content = JsonUtility.FromJson<AllPhrases>(contentPath);

        ListDictionary.totalPhrase = content.totalPhrase;
        ListDictionary.phraseList = content.phraseList;
    }

}
