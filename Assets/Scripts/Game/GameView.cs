using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public GameRun GameRun;
    public GameManager GameManager;

    public TMP_Text TipsText;
    public TMP_Text ChancesText;
    public TMP_Text TimerText;

    public TMP_Text TitleText;
    public TMP_Text PhraseText;

    public TimerConvert TimerConvert;

    void Awake()
    {
        GameManager.WordsCountEventHandler += GameRun_WordsCountEventHandler;

        GameRun.GamePhraseEventHanndler += GameRun_GamePhraseEventHanndler;
        GameRun.GameEndedEventHandler += GameRun_GameEndedEventHandler;

        GameRun.GameTipsEventHandler += GameRun_GameTipsEventHandler;
        GameRun.GameChancesEventHandler += GameRun_GameChancesEventHandler;

        GameManager.GameModesEventHandler += GameManager_GameModesEventHandler;
        GameManager.TimerEventHandler += GameManager_TimerEventHandler;
    }

    private void GameRun_WordsCountEventHandler(int length)
    {
        if (length > 0)
            TitleText.text = $"{length} letras restando";
        else if (length == -1)
            TitleText.text = $"Descubra a palavra";
        else if (length <= 2)
            TitleText.text = $"Última letra restando";
        else if (length == 0)
            TitleText.text = $"A palavra foi descoberta";

    }

    private void GameRun_GamePhraseEventHanndler(string phrase)
    {
        PhraseText.text = phrase;
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
            ChancesText.gameObject.SetActive(true);
        }
        else
        {
            ChancesText.gameObject.SetActive(false);
        }

        if (tipsOn)
        {
            TipsText.gameObject.SetActive(true);
        }
        else
        {
            TipsText.gameObject.SetActive(false);
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
        ChancesText.text = $"{chances}";
    }

    private void GameRun_GameTipsEventHandler(int tips)
    {
        TipsText.text = $"{tips}";
    }

    private void GameRun_GameEndedEventHandler(bool isWin)
    {
        if (isWin)
        {
            Debug.Log("Jogo vencido");
        }
        else
        {
            Debug.Log("Jogo perdido");
        }
    }
}
