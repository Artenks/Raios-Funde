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
        public List<string> blockedWords;

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

    public void UpdateDictionary()
    {
        if (ListDictionary.totalPhrase < 5)
        {
            var content = Dictionary.text.Split(" ");

            if (ListDictionary.blockedWords.Count >= content.Length)
            {
                ListDictionary.blockedWords.Clear();
            }

            do
            {
                var randomPhraseRank = Random.Range(0, content.Length);
                if (content[randomPhraseRank].Length == 5 && ListDictionary.totalPhrase < 10)
                {
                    bool continueLoop = true;
                    var phrase = content[randomPhraseRank];
                    var rightPhrase = "";

                    for (int i = 0; i <= ListDictionary.blockedWords.Count - 1; i++)
                    {
                        if (ListDictionary.blockedWords[i] == phrase)
                        {
                            Debug.Log("Essa palavra já foi vista");
                            continueLoop = false;
                            break;
                        }
                    }

                    if (!continueLoop)
                        continue;

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
                    ListDictionary.blockedWords.Add(phrase);
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

        UpdateDictionary();
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
        ListDictionary.blockedWords = content.blockedWords;
    }

}
