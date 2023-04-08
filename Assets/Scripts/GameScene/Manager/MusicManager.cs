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

    public AudioSource MusicSource;
    public Toggle MusicButton;

    public Slider SliderPreview;
    public TMP_Text SliderText;

    public GameOnFirstExecute GameOnFirstExecute;
    public EffectsManager EffectsManager;

    private void Awake()
    {
        GameOnFirstExecute.SoundResetStateEventHandler += GameOnFirstExecute_SoundsResetStateEventHandler;
    }

    private void GameOnFirstExecute_SoundsResetStateEventHandler()
    {
        MusicButton.isOn = true;
        MusicOn();

        Info.volume = 1f;
        SliderVolume(false);

        EffectsManager.EffectReset();
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
        MusicSource.volume = Info.volume;

        MusicEventHandler?.Invoke();
    }

    public void MusicOn()
    {
        if (MusicButton.isOn)
        {
            MusicSource.enabled = true;

            MusicSource.Play();
            Info.music = true;
        }
        else
        {
            MusicSource.Stop();
            Info.music = false;

            MusicSource.enabled = false;
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
