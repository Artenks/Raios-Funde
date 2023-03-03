using TMPro;
using UnityEngine;

public class EndGameView : MonoBehaviour
{
    public GameView GameView;
    public GameScoreView GameScoreView;
    public GameRun GameRun;

    public TMP_Text StreakText;
    public TMP_Text TimerText;
    public TMP_Text RecordText;
    public TMP_Text TitleText;
    public TMP_Text PhraseText;
    public TMP_Text SniperText;

    private void Awake()
    {
        GameView.GameStateEventHandler += GameView_GameStateEventHandler;
        GameView.GameTimerEventHandler += GameView_GameTimerEventHandler;

        GameScoreView.StreakScoreEventHandler += GameScoreView_StreakScoreEventHandler;

        GameRun.EndSniperUserEventHandler += GameRun_EndSniperUserEventHandler;
        GameRun.EndPhraseEventHandler += GameView_EndPhraseEventHandler;
    }

    private void GameRun_EndSniperUserEventHandler(string user)
    {
        SniperText.gameObject.SetActive(true);
        SniperText.text = $"{user} snipou a palavra!";
    }

    private void GameView_EndPhraseEventHandler(string censured, string phrase)
    {
        var output = "";

        for (var i = 0; i <= phrase.Length - 1; i++)
        {
            if (censured[i] == phrase[i])
            {
                output += $"<b><u>{phrase[i]}</u></b>";
                continue;
            }

            output += $"<color=#FFD500><b><u>{phrase[i]}</u><b></color>";
        }

        PhraseText.text = output;
        PhraseText.gameObject.SetActive(true);
    }

    private void GameScoreView_StreakScoreEventHandler(int streak, int record)
    {
        if (streak > 0)
        {
            StreakText.gameObject.SetActive(true);
            StreakText.text = $"Combo: {streak}";
        }
        else
            StreakText.gameObject.SetActive(false);

        if (record > 0)
        {
            RecordText.gameObject.SetActive(true);
            RecordText.text = $"Record: {record}";
        }
        else
            RecordText.gameObject.SetActive(true);

    }

    private void GameView_GameTimerEventHandler(string timerText)
    {
        TimerText.text = timerText;
    }

    private void GameView_GameStateEventHandler(bool isWin)
    {
        if (isWin)
            TitleText.text = "Acertou a palavra!";
        else
            TitleText.text = "A palavra era:";
    }
}
