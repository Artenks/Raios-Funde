using TMPro;
using UnityEngine;

public class DebugScripts : MonoBehaviour
{
    public GameView GameView;

    [SerializeField] TMP_Text FpsText;
    [SerializeField] TMP_Text LogText;
    [SerializeField] GameObject LogPanelObject;

    private string oldPhrase = null;

    void Awake()
    {
        GameView.DebugPhraseEventHandler += GameView_DebugPhraseEventHandler;
    }

    private void GameView_DebugPhraseEventHandler(string phrase, string fullPhrase)
    {
        if (oldPhrase != fullPhrase)
        {
            if (fullPhrase == "")
                return;

            oldPhrase = fullPhrase;
            LogText.text += $"Palavra:{fullPhrase}.\r\n";
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(CalculateFps), 0, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown("f1"))
        {
            if (LogPanelObject.activeInHierarchy)
            {
                LogPanelObject.SetActive(false);

            }
            else
            {
                LogPanelObject.SetActive(true);
            }
        }
    }

    private void CalculateFps()
    {
        if (LogPanelObject.activeInHierarchy)
        {
            var fps = 1f / Time.deltaTime;
            FpsText.text = $"{fps.ToString("00")} Fps";
        }

    }

}
