using System;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public event Action<string, string> DebugPhraseEventHandler;

    public event Action<string> GameTimerEventHandler;
    public event Action<bool> GameStateEventHandler;

    public GameRun GameRun;
    public GameManager GameManager;

    public SimilarLetters SimilarLetters;
    public TMP_Text AnagramText;

    public GameObject TipsText;
    public GameObject ChancesText;
    public TMP_Text TimerText;

    //public TMP_Text TitleText;
    public TMP_Text PhraseText;

    public TimerConvert TimerConvert;

    public DebugScripts DebugScripts;


    void Awake()
    {
        //GameManager.WordsCountEventHandler += GameRun_WordsCountEventHandler;

        GameRun.GamePhraseEventHanndler += GameRun_GamePhraseEventHanndler;
        GameRun.AnagramPhraseEventHandler += GameRun_AnagramPhraseEventHanndler;
        GameRun.GameEndedEventHandler += GameRun_GameEndedEventHandler;

        GameRun.GameTipsEventHandler += GameRun_GameTipsEventHandler;
        GameRun.GameChancesEventHandler += GameRun_GameChancesEventHandler;

        GameManager.GameModesEventHandler += GameManager_GameModesEventHandler;
        GameManager.TimerEventHandler += GameManager_TimerEventHandler;
    }

    //private void GameRun_WordsCountEventHandler(int length)
    //{
    //    if (length > 0)
    //        TitleText.text = $"{length} letras restando";
    //    else if (length == -1)
    //        TitleText.text = $"Descubra a palavra";
    //    else if (length <= 2)
    //        TitleText.text = $"Última letra restando";
    //    else if (length == 0)
    //        TitleText.text = $"A palavra foi descoberta";

    //}

    private void GameRun_GamePhraseEventHanndler(string phrase, string fullPhrase)
    {
        PhraseText.text = phrase;
        DebugPhraseEventHandler?.Invoke(phrase, fullPhrase);
    }

    public void GameRun_AnagramPhraseEventHanndler(string message, string phrase, bool clearString)
    {
        if (clearString)
        {
            AnagramText.text = "";
            return;
        }

        if (SimilarLetters.IsSimilar(message, phrase))
        {
            AnagramText.text = SimilarLetters.FoundSimilars(message, phrase);
        }
    }

    private void GameManager_TimerEventHandler(float timer)
    {
        TimerText.text = TimerConvert.TimerConverted(timer);
    }

    private void Start()
    {
        GameManager_GameModesEventHandler(GameManager.Modes.ChancesOn, GameManager.Modes.TipsOn, GameManager.Modes.TimerOn);
        GameManager_TimerEventHandler(GameManager.Data.Timer);
    }

    private void GameManager_GameModesEventHandler(bool chancesOn, bool tipsOn, bool timerOn)
    {
        if (chancesOn)
        {
            ChancesText.GetComponentInParent<TMP_Text>(true).gameObject.SetActive(true);
        }
        else
        {
            ChancesText.GetComponentInParent<TMP_Text>(true).gameObject.SetActive(false);
        }

        if (tipsOn)
        {
            TipsText.GetComponentInParent<TMP_Text>(true).gameObject.SetActive(true);
        }
        else
        {
            TipsText.GetComponentInParent<TMP_Text>(true).gameObject.SetActive(false);
        }

        if (timerOn)
        {
            TimerText.gameObject.SetActive(true);
        }
        else
        {
            TimerText.gameObject.SetActive(false);
        }
    }

    private void GameRun_GameChancesEventHandler(int chances)
    {
        ChancesText.GetComponentInChildren<TMP_Text>(true).text = $"{chances}";
    }

    private void GameRun_GameTipsEventHandler(int tips)
    {
        TipsText.GetComponentInChildren<TMP_Text>(true).text = $"{tips}";
    }

    private void GameRun_GameEndedEventHandler(bool isWin)
    {
        GameTimerEventHandler?.Invoke(TimerText.text);
        if (isWin)
        {
            //Debug.Log("Jogo vencido");
            GameStateEventHandler?.Invoke(true);
        }
        else
        {
            //Debug.Log("Jogo perdido");
            GameStateEventHandler?.Invoke(false);
        }
    }
}
