using UnityEngine;

public class PlayEffect : MonoBehaviour
{
    public AudioClip Clip;
    private AudioSource _source;

    public void PlayEffectSound()
    {
        if (!gameObject.activeInHierarchy)
            return;

        _source.clip = Clip;
        _source.Play();
    }
    private void Awake()
    {
        _source = GameObject.FindGameObjectWithTag("EffectSounds").GetComponent<AudioSource>();
    }

}
