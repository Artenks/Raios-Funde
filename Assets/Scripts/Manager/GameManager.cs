using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<bool> GameScoreEventHandler;
    public event Action ReceiveAPhraseEventHandler;
    public event Action<bool, bool, bool> GameModesEventHandler;
    public event Action<float> TimerEventHandler;

    public TogetherGame TogetherMode;
    public GameModesInfo ModeInfo;
    public GameRun GameRun;

    public GameObject FinishBox;
    public GameObject TogetherBox;
    public GameObject CreateBox;

    public PhraseFromDictionary PhraseFromDictionary;

    public enum PlayModes
    {
        None,
        SimpleMode,
        CreateMode
    }
    [Serializable]
    public enum GameState
    {
        Coding,
        Waiting,
        Playing,
        Win,
        Lost,
        End,
        Restart,
    }
    [Serializable]
    public struct GameData
    {
        public GameState State;
        public PlayModes PlayMode;
        public float Timer;
        public int Tips;
        public int Chances;
    }
    public GameData Data;

    [Serializable]
    public struct GameModesBool
    {
        public bool TimerOn;
        public bool TipsOn;
        public bool ChancesOn;
    }
    public GameModesBool Modes;

    private TimeForUpdate _timeForUpdate;

    public string TakeTogetherPhrase()
    {
        ReceiveAPhraseEventHandler?.Invoke();
        return PhraseFromDictionary.Phrase;
    }

    private void ConfigurateGame()
    {
        ResetCounter();
        ConfigurateGameModes();

        Data.State = GameState.Waiting;
    }

    public void ConfigurateGameModes()
    {
        Data.Timer = ModeInfo.Info.Timer;
        Data.Tips = ModeInfo.Info.Tips;
        Data.Chances = ModeInfo.Info.Chances;

        VerifyModesOn();
    }

    private void ResetCounter()
    {
        _timeForUpdate.Data.TotalTimer = 0;
        _timeForUpdate.Data.NowTimer = 0;
        _timeForUpdate.Data.RunTimer = false;
    }

    private void TogetherGame_DisableEventHandler()
    {
        Data.State = GameState.Coding;
    }

    private void VerifyModesOn()
    {
        if (Data.Chances != 0)
            Modes.ChancesOn = true;
        else
            Modes.ChancesOn = false;

        if (Data.Tips != 0)
            Modes.TipsOn = true;
        else
            Modes.TipsOn = false;

        if (Data.Timer != 0.0)
            Modes.TimerOn = true;
        else
            Modes.TimerOn = false;

        GameModesEventHandler?.Invoke(Modes.ChancesOn, Modes.TipsOn, Modes.TimerOn);
    }

    private void Awake()
    {
        Data.State = GameState.Coding;
        _timeForUpdate = GetComponent<TimeForUpdate>();

        TogetherMode.DisableEventHandler += TogetherGame_DisableEventHandler;
    }

    private void Update()
    {
        switch (Data.State)
        {
            case GameState.Coding:
            case GameState.Restart:
                ConfigurateGame();
                break;
            case GameState.Waiting:
                TimerEventHandler?.Invoke(Data.Timer);
                GameRun.UpdateOnEnable();
                break;

            case GameState.Playing:
                if (Modes.TimerOn)
                {
                    var timer = _timeForUpdate.StartTheCount(Data.Timer);
                    TimerEventHandler?.Invoke(_timeForUpdate.Data.NowTimer);

                    if (timer)
                    {
                        Data.State = GameState.Lost;
                        GameRun.TimerOver();
                    }
                }
                break;

            case GameState.Lost:
            case GameState.Win:

                if (Data.State == GameState.Win)
                {
                    GameScoreEventHandler?.Invoke(true);
                }
                else
                    GameScoreEventHandler?.Invoke(false);

                TogetherBox.SetActive(false);
                CreateBox.SetActive(false);
                FinishBox.SetActive(true);

                Data.State = GameState.End;
                break;
        }
    }
}
