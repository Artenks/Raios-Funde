using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxColor : MonoBehaviour
{
    public SimilarLetters SimilarLetters;
    public Image BoxImage;

    private GameRun _gameInfos;
    private string _phrase;
    private void Awake()
    {
        _gameInfos = GameObject.FindGameObjectWithTag("GameInfos").GetComponent<GameRun>();
        _phrase = _gameInfos.DataGame.Phrase;
    }

    public void ChangeBoxColor(string userMessage)
    {
        if (_gameInfos == null || !_gameInfos.gameObject.activeInHierarchy)
            return;

        if (_phrase.ToLower() == userMessage.ToLower())
        {
            BoxImage.color = new Color32(167, 255, 0, 255);
            Debug.Log("palavra certa");
        }
        else
        {
            if (SimilarLetters.IsSimilar(userMessage, _phrase))
            {
                Debug.Log("palavra similar");
                BoxImage.color = new Color32(255, 216, 0, 255);
                SimilarLetters.FoundSimilars(userMessage, _phrase);
            }
            else
            {
                BoxImage.color = new Color32(255, 0, 66, 255);
                Debug.Log("palavra nao similar");
            }
        }

    }
}
