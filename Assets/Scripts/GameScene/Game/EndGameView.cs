using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    public GameView GameView;
    public GameScoreView GameScoreView;
    public GameRun GameRun;
    public GameManager GameManager;

    public TMP_Text StreakText;
    public TMP_Text TimerText;
    public TMP_Text RecordText;
    public TMP_Text TitleText;
    public TMP_Text PhraseText;
    public TMP_Text SniperText;

    private string RightColor = "#FFD500";
    private string WrongColor = "#FF1124";


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
        SniperText.GetComponentInParent<Image>(true).gameObject.SetActive(true);
        SniperText.text = $"{user} é uma lenda!";
    }

    private void GameView_EndPhraseEventHandler(string censured, string phrase, bool rightPhrase)
    {
        var output = "";

        switch (rightPhrase)
        {
            case true:
                for (var i = 0; i <= phrase.Length - 1; i++)
                {
                    if (censured[i] == '_')
                    {
                        output += $"<color={RightColor}><b><u>{phrase[i]}</u><b></color>";
                        continue;
                    }
                    output += $"<b><u>{phrase[i]}</u></b>";
                }
                break;
            case false:
                for (var i = 0; i <= phrase.Length - 1; i++)
                {
                    if (censured[i] == phrase[i])
                    {
                        output += $"<b><u>{phrase[i]}</u></b>";
                        continue;
                    }

                    output += $"<color={RightColor}><b><u>{phrase[i]}</u><b></color>";
                }
                break;
        }

        PhraseText.text = output;
        if (!rightPhrase)
            ChangePhraseColor();

        PhraseText.gameObject.SetActive(true);
    }

    private void ChangePhraseColor()
    {
        var phrase = PhraseText.text;
        PhraseText.text = phrase.Replace($"{RightColor}", $"{WrongColor}");
    }

    private void GameScoreView_StreakScoreEventHandler(int streak, int record)
    {
        if (streak > 0)
        {
            StreakText.GetComponentInParent<Image>(true).gameObject.SetActive(true);
            StreakText.text = $"{streak}";

        }
        else
            StreakText.GetComponentInParent<Image>(true).gameObject.SetActive(false);

        if (record > 0)
        {
            RecordText.GetComponentInParent<Image>(true).gameObject.SetActive(true);
            RecordText.text = $"{record}";
        }
        else
            RecordText.GetComponentInParent<Image>(true).gameObject.SetActive(false);

    }

    private void GameView_GameTimerEventHandler(string timerText)
    {
        if (GameManager.Modes.TimerOn)
        {
            TimerText.text = timerText;
            TimerText.gameObject.SetActive(true);
        }
        else
        {
            TimerText.gameObject.SetActive(false);
        }
    }

    private void GameView_GameStateEventHandler(bool isWin)
    {
        if (isWin)
            TitleText.text = "Lenda do Raios Funde";
        else
            TitleText.text = "Fracasso do Raios Funde";
    }
}
