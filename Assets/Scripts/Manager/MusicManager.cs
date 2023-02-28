using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public event Action MusicEventHandler;

    [Serializable]
    public struct AudioInfo
    {
        public bool music;
        public float volume;
    }
    public AudioInfo Info;

    public AudioSource CaretakerMusic;
    public Toggle MusicButton;

    public Slider SliderPreview;
    public TMP_Text SliderText;

    public GameOnFirstExecute GameOnFirstExecute;

    private void Awake()
    {
        GameOnFirstExecute.MusicResetStateEventHandler += GameOnFirstExecute_MusicResetStateEventHandler;
    }

    private void GameOnFirstExecute_MusicResetStateEventHandler()
    {
        MusicButton.isOn = true;
        MusicOn();

        Info.volume = 1f;
        SliderVolume(false);
    }

    public void SliderVolume(bool updateVolume)
    {
        if (updateVolume)
        {
            float slidePorcent = ((SliderPreview.value)) * 100;
            int slidePorcentValue = (int)(slidePorcent);

            SliderText.text = $"{slidePorcentValue}%";
            Info.volume = SliderPreview.value;
        }
        else
        {
            float slidePorcent = ((Info.volume)) * 100;
            int slidePorcentValue = (int)(slidePorcent);

            SliderPreview.value = Info.volume;

            SliderText.text = $"{slidePorcentValue}%";
        }
        CaretakerMusic.volume = Info.volume / 2;

        MusicEventHandler?.Invoke();
    }

    public void MusicOn()
    {
        if (MusicButton.isOn)
        {
            CaretakerMusic.Play();
            Info.music = true;
        }
        else
        {
            CaretakerMusic.Stop();
            Info.music = false;
        }
        MusicEventHandler?.Invoke();
    }

    public void ConfigurateMusicOnOpen()
    {
        SliderVolume(false);
        MusicButton.isOn = Info.music;
        MusicOn();
    }


}
