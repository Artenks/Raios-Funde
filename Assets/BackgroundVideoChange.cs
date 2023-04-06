using System;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundVideoChange : MonoBehaviour
{
    public bool MakeTransition;

    public VideoClip TransitionVideo;
    public VideoClip CalmVideo;
    public VideoClip AngryVideo;

    [Serializable]
    public enum BackgroundMode
    {
        Transition,
        Angry,
        Calm
    }

    public BackgroundMode Mode;

    private VideoPlayer _videoPlayer;
    private Animator _animator;

    private double _videoLength;
    private double _currentTime;

    public void BackgroundAnimation(bool isAngry)
    {
        if (isAngry)
        {
            if (_animator.GetInteger("Change") == 1)
            {
                _animator.SetTrigger("Repeat");
            }
            _animator.SetInteger("Change", 1);
        }
        else
        {
            if (_animator.GetInteger("Change") == 0)
            {
                _animator.SetTrigger("Repeat");
            }
            _animator.SetInteger("Change", 0);
        }
    }

    private void Awake()
    {

        _videoPlayer = GetComponent<VideoPlayer>();
        _animator = GetComponent<Animator>();

        _animator.SetInteger("Change", 0);
    }
    private void Update()
    {
        if (MakeTransition)
            ChangeBackground();

        _currentTime = _videoPlayer.time;
    }

    private void ChangeBackground()
    {
        _videoLength = _videoPlayer.clip.length - 0.5f;

        if (Mode == BackgroundMode.Transition)
        {
            _videoPlayer.clip = TransitionVideo;
        }
        else if (Mode == BackgroundMode.Calm)
        {
            _videoPlayer.clip = CalmVideo;
        }
        else if (Mode == BackgroundMode.Angry)
        {
            _videoPlayer.clip = AngryVideo;
        }
        MakeTransition = false;
    }
}
