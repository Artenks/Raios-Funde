using UnityEngine;

public class GameButtonModes : MonoBehaviour
{
    public GameRun GameRun;
    public GameManager GameManager;

    public enum ModesStatus
    {
        None,
        CreateMode,
        TogetherMode
    }
    public ModesStatus Modes;

    public void DeterminateMode()
    {
        GameRun.DataGame.Phrase = "";

        if (Modes == ModesStatus.TogetherMode)
            GameManager.Data.PlayMode = GameManager.PlayModes.TogetherMode;

        if (Modes == ModesStatus.CreateMode)
            GameManager.Data.PlayMode = GameManager.PlayModes.CreateMode;
    }
}
