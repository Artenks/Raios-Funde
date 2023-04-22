using System;
using UnityEngine;

public class SimpleGame : MonoBehaviour
{
    public event Action DisableEventHandler;

    public GameManager GameManager;
    public GameRun GameRunning;

    public TimeInGame TimeInGame;

    [Serializable]
    public struct UserData
    {
        public string User;
        public string Message;
    }
    public UserData UserDataInGame;

    public void Together_GameModesUpdate()
    {
        if (GameRunning.DataGame.Phrase == "" && GameManager.Data.PlayMode == GameManager.PlayModes.SimpleMode)
        {
            GameRunning.DataGame.Phrase = GameManager.TakeTogetherPhrase();
        }

        GameRunning.DataGame.PhraseCensured = "";
        for (var i = GameRunning.DataGame.Phrase.Length - 1; i >= 0; i--)
        {
            GameRunning.DataGame.PhraseCensured += "_";
        }

        GameRunning.DataGame.Tips = GameManager.Data.Tips;
        GameRunning.DataGame.Chances = GameManager.Data.Chances;
    }

    public void OnChatMessage(string user, string message)
    {
        UserDataInGame.User = user.ToLower();
        UserDataInGame.Message = message.ToLower();

        //permanece no fim da linha
        if (GameManager.Data.State == GameManager.GameState.Lost)
            return;
        if (GameManager.Data.State == GameManager.GameState.Win)
            return;

        if (this.gameObject.activeInHierarchy && GameManager.Data.State != GameManager.GameState.Playing)
        {
            GameManager.Data.State = GameManager.GameState.Playing;
        }

        GameRunning.PlayingGame(user.ToLower(), message.ToLower());
    }

    private void Update()
    {
        if (GameManager.Data.State == GameManager.GameState.Waiting)
        {
            Together_GameModesUpdate();
        }
    }

    private void OnEnable()
    {
        DisableEventHandler?.Invoke();
    }
    private void OnDisable()
    {
        if (GameManager.Data.State == GameManager.GameState.Playing)
            GameManager.Data.State = GameManager.GameState.Coding;

    }
}
