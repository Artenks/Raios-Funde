using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectsManager : MonoBehaviour
{
    public event Action EffectsEventHandler;

    [Serializable]
    public struct AudioInfo
    {
        public bool effects;
        public float volume;
    }
    public AudioInfo Info;

    public AudioSource EffectsSource;
    public Toggle EffectButton;

    public Slider SliderPreview;
    public TMP_Text SliderText;

    public void SliderVolume(bool updateVolume)
    {
        if (updateVolume)
        {
            float slidePorcent = ((SliderPreview.value)) * 100 / 2;
            ;
            int slidePorcentValue = (int)(slidePorcent);

            SliderText.text = $"{slidePorcentValue}%";
            Info.volume = SliderPreview.value;
        }
        else
        {
            float slidePorcent = ((Info.volume)) * 100 / 2;
            int slidePorcentValue = (int)(slidePorcent);

            SliderPreview.value = Info.volume;

            SliderText.text = $"{slidePorcentValue}%";
        }
        EffectsSource.volume = Info.volume;

        EffectsEventHandler?.Invoke();
    }

    public void EffectsOn()
    {
        if (EffectButton.isOn)
        {
            EffectsSource.enabled = true;

            EffectsSource.volume = Info.volume;
            EffectsSource.Play();
            Info.effects = true;

        }
        else
        {
            EffectsSource.Stop();
            Info.effects = false;

            EffectsSource.volume = 0;
            EffectsSource.enabled = false;
        }
        EffectsEventHandler?.Invoke();
    }
    public void EffectReset()
    {
        EffectButton.isOn = true;
        EffectsOn();

        Info.volume = 0.3f;
        SliderVolume(false);
    }
    public void ConfigurateEffectOnOpen()
    {
        SliderVolume(false);
        EffectButton.isOn = Info.effects;
        EffectsOn();
    }
}
