using TMPro;
using UnityEngine;

public class CreatePhraseGame : MonoBehaviour
{
    public GameRun GameRunning;
    public GameManager GameManager;
    public TMP_InputField InputField;

    public void PhraseCreated()
    {
        GameManager.Data.PlayMode = GameManager.PlayModes.CreateMode;
    }

    public void PhraseIsStart()
    {
        var output = "";
        for (int i = 0; i <= InputField.text.Length - 1; i++)
        {
            if (i == 0)
            {
                output += InputField.text[i].ToString().ToUpper();
                continue;
            }
            output += InputField.text[i];
        }

        GameRunning.DataGame.Phrase = output;
        InputField.text = null;
    }

    public void DeletePhrase()
    {
        GameRunning.DataGame.Phrase = null;
    }
}
