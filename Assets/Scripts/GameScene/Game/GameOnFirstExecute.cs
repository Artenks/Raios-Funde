using System;
using System.Collections.Generic;
using UnityEngine;
public class GameOnFirstExecute : MonoBehaviour
{
    public event Action SoundResetStateEventHandler;

    public ConfigSave ConfigSave;
    public ResolutionScript ResolutionScript;
    public MusicManager MusicManager;

    public List<GameObject> ObjectsToDisable;
    public GameObject SceneInFirstTime;

    public void FirstExecuteInvoke()
    {
        if (ConfigSave.Info.noIsFirstTimePlaying == false)
        {
            SceneInFirstTime.SetActive(true);
            RemoveObjectsOnFirstTime();

            SoundResetStateEventHandler?.Invoke();
            ResolutionScript.ResolutionOnFirstExecute();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void RemoveObjectsOnFirstTime()
    {
        foreach (GameObject obj in ObjectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
