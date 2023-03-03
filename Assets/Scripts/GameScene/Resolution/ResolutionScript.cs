using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour
{
    public event Action ResolutionEventHandler;

    [Serializable]
    public struct ResolutionInfo
    {
        public int width;
        public int height;
        public float frameRate;
        public bool isFullscreen;
    }
    public ResolutionInfo Info;

    public TMP_Dropdown WindowResolution;
    public Toggle FullscreenButton;

    public GameOnFirstExecute GameOnFirstExecute;
    private TimeForUpdate _timeForUpdate;

    private List<string> dropDown;
    private bool _timeToSave;
    public void ResolutionFullscreen()
    {
        if (FullscreenButton.isOn)
        {
            Screen.fullScreen = true;
            Info.isFullscreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            Info.isFullscreen = false;
        }

        ResolutionEventHandler?.Invoke();
    }

    public void SetResolution(bool takeFromDropdown)
    {
        var resolution = WindowResolution.captionText.text.Split("x");
        if (takeFromDropdown)
        {
            Info.width = int.Parse(resolution[0]);
            Info.height = int.Parse(resolution[1]);
        }
        else
        {
            Info.width = Screen.width;
            Info.height = Screen.height;
        }
        Screen.SetResolution(Info.width, Info.height, Info.isFullscreen);

        ResolutionEventHandler?.Invoke();
    }
    public void ConfigurateResolutionOpen()
    {
        FullscreenButton.isOn = Info.isFullscreen;
        ResolutionFullscreen();
    }
    public void ResolutionOnFirstExecute()
    {
        FullscreenButton.isOn = true;
        ResolutionFullscreen();

        Info.width = Display.main.systemWidth;
        Info.height = Display.main.systemHeight;

        SetResolution(false);
    }

    private void Awake()
    {
        _timeForUpdate = GetComponent<TimeForUpdate>();

        Resolution[] allResolutions = Screen.resolutions;
        foreach (var res in allResolutions)
        {
            if (res.refreshRate == 60)
            {
                dropDown = new List<string> { $"{res.width}x{res.height}" };
                WindowResolution.AddOptions(dropDown);

                if (Info.frameRate != 60)
                    Info.frameRate = 60;
            }
        }

    }

    private void Update()
    {
        if (WindowResolution.captionText.text != $"{Screen.width}x{Screen.height}")
        {

            Info.width = Screen.width;
            Info.height = Screen.height;
            _timeToSave = true;
            _timeForUpdate.Data.TotalTimer += 0.1f;

            WindowResolution.captionText.SetText($"{Info.width}x{Info.height}");
        }

        if (_timeToSave)
        {
            var complete = _timeForUpdate.StartTheCount(1f);

            if (complete)
            {
                ResolutionEventHandler?.Invoke();

                _timeToSave = false;
            }
        }

    }
}
