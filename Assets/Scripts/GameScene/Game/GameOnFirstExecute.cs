using System;
using UnityEngine;
public class GameOnFirstExecute : MonoBehaviour
{
    public event Action FirstTimeEventHandler;

    public event Action MusicResetStateEventHandler;

    public ConfigSave ConfigSave;
    public ResolutionScript ResolutionScript;
    public MusicManager MusicManager;

    public void FirstExecuteInvoke()
    {
        if (ConfigSave.Info.noIsFirstTime == false)
        {
            MusicResetStateEventHandler?.Invoke();
            ResolutionScript.ResolutionOnFirstExecute();
            FirstTimeEventHandler?.Invoke();
        }
        Destroy(this.gameObject);
    }
}
