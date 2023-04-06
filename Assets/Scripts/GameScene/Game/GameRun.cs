using System;
using UnityEngine;

public class GameRun : MonoBehaviour
{
    public event Action<string, string> GamePhraseEventHanndler;
    public event Action<string, string, bool> AnagramPhraseEventHandler;
    public event Action<bool> GameEndedEventHandler;
    public event Action<int> GameTipsEventHandler;
    public event Action<int> GameChancesEventHandler;
    public event Action<string> EndSniperUserEventHandler;

    public event Action<string, string, bool> EndPhraseEventHandler;

    public GameManager GameManager;
    public RankInfo RankInfo;

    public CreateAnagram CreateAnagram;

    public PhraseInDictionary PhraseInDictionary;
    public CaractereRemove CaractereRemove;

    [Serializable]
    public struct DataInGame
    {
        public string Phrase;
        public string PhraseCensured;
        public int Tips;
        public int Chances;
    }
    public DataInGame DataGame;

    public DebugScripts DebugScripts;

    public void UpdateOnEnable()
    {
        GamePhraseEventHanndler?.Invoke(DataGame.PhraseCensured, DataGame.Phrase);
        AnagramPhraseEventHandler?.Invoke(" ", " ", true);
        GameTipsEventHandler?.Invoke(DataGame.Tips);
        GameChancesEventHandler?.Invoke(DataGame.Chances);

        CreateAnagram.ConstructPhraseAnagram(DataGame.Phrase);
    }

    private string _lowerPhrase;

    private bool LastWords()
    {
        var lastWord = 0;
        for (var i = 0; i <= DataGame.PhraseCensured.Length - 1; i++)
        {
            if (DataGame.PhraseCensured[i] == '_')
            {
                lastWord++;
            }
        }
        return lastWord <= 2 ? true : false;
    }

    private string UpdateCensuredPhrase(string user, string message, bool tipsOneChar)
    {
        var output = "";
        var lettersTaked = 0;
        if (tipsOneChar)
        {
            var charMessage = char.Parse(CaractereRemove.RemoveDiacritics(message));

            for (var i = 0; i <= DataGame.Phrase.Length - 1; i++)
            {
                if ($"{DataGame.Phrase[i].ToString().ToLower()}" == $"{charMessage}" || $"{_lowerPhrase[i]}" == $"{charMessage}")
                {
                    output += $"{DataGame.Phrase[i]}";
                    lettersTaked++;
                }
                else
                {
                    output += DataGame.PhraseCensured[i];
                }
            }
        }
        else
        {
            for (var i = 0; i <= DataGame.Phrase.Length - 1; i++)
            {
                if ($"{message[i].ToString().ToLower()}" == $"{DataGame.Phrase[i].ToString().ToLower()}" || message[i] == _lowerPhrase[i])
                {
                    output += $"{DataGame.Phrase[i]}";
                    lettersTaked++;
                }
                else
                {
                    output += DataGame.PhraseCensured[i];
                }
            }
        }

        PlayerSniperLetter(user, lettersTaked);

        return output;
    }

    public void TimerOver()
    {
        GameEndedEventHandler?.Invoke(false);
    }

    public void PlayingGame(string user, string message)
    {
        if (GameManager.Data.State == GameManager.GameState.Playing)
        {
            var wrongPhrase = true;
            _lowerPhrase = CaractereRemove.RemoveDiacritics(DataGame.Phrase.ToLower());

            if (message == DataGame.Phrase.ToLower() || message == _lowerPhrase)
            {
                wrongPhrase = false;

                if (RankInfo.UserExist(user))
                {
                    RankInfo.UpdateUserScore(user, PlayerSniperPhrase());
                }
                else
                {
                    RankInfo.AddAUser(user, PlayerSniperPhrase());
                }
                EndPhraseEventHandler?.Invoke(DataGame.PhraseCensured, DataGame.Phrase, true);
                DataGame.PhraseCensured = DataGame.Phrase;
                EndSniperUserEventHandler?.Invoke(user);
            }

            if (!LastWords())
            {
                var oldTips = DataGame.Tips;

                if (DataGame.Tips > 0 || !GameManager.Modes.TipsOn)
                {
                    if (message.Length == DataGame.Phrase.Length || message.Length == 1)
                    {
                        bool tipsOneChar = message.Length == 1;
                        DataGame.PhraseCensured = UpdateCensuredPhrase(user, message, tipsOneChar);
                    }

                    AnagramPhraseEventHandler?.Invoke(message.ToLower(), DataGame.Phrase, false);
                }

                if (DataGame.Phrase.Length == message.Length || message.Length == 1)
                {
                    if (DataGame.Tips > 0 && wrongPhrase)
                    {
                        DataGame.Tips--;
                    }

                    if (oldTips != DataGame.Tips)
                    {
                        GameTipsEventHandler?.Invoke(DataGame.Tips);
                    }
                }

            }

            if (GameManager.Modes.ChancesOn)
            {
                if (DataGame.Phrase.Length == message.Length)
                {
                    var oldChances = DataGame.Chances;

                    if (wrongPhrase)
                    {
                        DataGame.Chances--;
                    }

                    if (oldChances != DataGame.Chances)
                    {
                        GameChancesEventHandler?.Invoke(DataGame.Chances);
                    }

                    if (DataGame.Chances < 0)
                    {
                        GameEndedEventHandler?.Invoke(false);
                        GameManager.Data.State = GameManager.GameState.Lost;
                    }
                }
            }

            GamePhraseEventHanndler?.Invoke(DataGame.PhraseCensured, DataGame.Phrase);
            if (DataGame.PhraseCensured != DataGame.Phrase)
            {
                EndPhraseEventHandler?.Invoke(DataGame.PhraseCensured, DataGame.Phrase, false);
            }
        }
    }

    private int PlayerSniperPhrase()
    {
        var score = 1;
        for (var i = DataGame.PhraseCensured.Length - 1; i >= 0; i--)
        {
            if (DataGame.PhraseCensured[i] == '_')
            {
                score += 1;
            }
        }
        return score;
    }
    private void PlayerSniperLetter(string user, int score)
    {
        if (RankInfo.UserExist(user))
        {
            RankInfo.UpdateUserScore(user, score);
        }
        else
        {
            RankInfo.AddAUser(user, score);
        }
    }

    private void Awake()
    {
        UpdateOnEnable();
    }

    private void Update()
    {
        if (GameManager.Data.State != GameManager.GameState.Playing)
            return;

        if (DataGame.PhraseCensured == DataGame.Phrase)
        {
            GameEndedEventHandler?.Invoke(true);
            GameManager.Data.State = GameManager.GameState.Win;
        }
    }
}
