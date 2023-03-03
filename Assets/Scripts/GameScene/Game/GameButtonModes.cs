using UnityEngine;

public class GameButtonModes : MonoBehaviour
{
    public GameRun GameRun;
    public GameManager GameManager;

    public enum ModesStatus
    {
        None,
        CreateMode,
        SimpleMode
    }
    public ModesStatus Modes;

    public void DeterminateMode()
    {
        GameRun.DataGame.Phrase = "";

        if (Modes == ModesStatus.SimpleMode)
            GameManager.Data.PlayMode = GameManager.PlayModes.SimpleMode;

        if (Modes == ModesStatus.CreateMode)
            GameManager.Data.PlayMode = GameManager.PlayModes.CreateMode;
    }
}
