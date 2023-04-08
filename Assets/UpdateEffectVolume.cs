using UnityEngine;
using UnityEngine.Video;

public class UpdateEffectVolume : MonoBehaviour
{
    public EffectsManager EffectsManager;
    private VideoPlayer _videoPlayer;
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        EffectsManager.EffectsEventHandler += EffectsManager_EffectsEventHandler;
    }

    private void EffectsManager_EffectsEventHandler()
    {
        _videoPlayer.SetDirectAudioVolume(0, EffectsManager.EffectsSource.volume);
    }
}
