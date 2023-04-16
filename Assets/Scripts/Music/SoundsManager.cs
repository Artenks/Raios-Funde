using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public bool InitialVolume;
    private float _volume;

    // General playlist variables
    [Header("Playlist Parameters")]
    public MusicPlaylist Playlists;

    // General playlist execution mode
    public bool RepeatPlaylists;

    [Serializable]
    public enum SceneMusicTemplate
    {
        game,
        menu,
        lost,
        win
    }
    public SceneMusicTemplate SceneMusic;

    [Header("General Parameters")]
    public float FadeDuration;
    public bool PlayOnAwake;

    // Counter of current song in execution inside a playlist
    private int _songCounter;
    private bool _changing = false;

    public AudioSource _source;

    private void Start()
    {
        // Tweak audio source parameters
        _source.playOnAwake = true;

        if (!InitialVolume)
            _volume = GetComponent<MusicManager>().Info.volume;
        else
            _volume = 0.3f;

        // Shuffle Playlists
        //if (ShufflePlaylists)
        //{
        //    Playlists = Shuffle(Playlists);
        //}

        // Start playlist play
        if (PlayOnAwake)
            PlayAllTracks();
    }

    private void Update()
    {
        if (_changing)
        {
            ChangePlaylist(null, 0);
        }
    }

    public void UpdateVolume()
    {
        _volume = GetComponent<MusicManager>().Info.volume;
    }

    public void PlayAllTracks()
    {
        StopAllCoroutines();
        StartCoroutine(PlayPlaylist(Playlists));
    }


    private IEnumerator PlayPlaylist(MusicPlaylist targetPlaylist)
    {
        // Shuffle target playlist if it is required
        if (targetPlaylist.Shuffle)
        {
            targetPlaylist.MusicList = Shuffle(targetPlaylist.MusicList);
        }

        // Execute target playlist until it finishes
        _songCounter = 0;
        while (_songCounter < targetPlaylist.MusicList.Count)
        {
            // Execute current song
            yield return StartCoroutine(PlaySongE(targetPlaylist.MusicList[_songCounter]));

            // Move to next song
            _songCounter++;
        }

    }

    public void ChangePlaylist(MusicPlaylist playlist, float endFadeDuration)
    {
        if (playlist != null)
        {
            Playlists = playlist;
        }

        if (_source.isPlaying)
        {
            if (!_changing)
            {
                Stop(true, endFadeDuration);
                _changing = true;
            }
        }
        else
        {
            PlayAllTracks();
            _changing = false;
        }

    }
    public void Stop(bool fade, float endFadeDuration)
    {
        StopAllCoroutines();
        if (fade)
        {
            StartCoroutine(StopWithFade(endFadeDuration));
        }
        else
            _source.Stop();
    }

    public void Next()
    {
        _source.Stop();
    }

    private IEnumerator StopWithFade(float endFadeDuration)
    {
        if (endFadeDuration == 0)
        {
            if (FadeDuration > 0)
            {
                float lerpValue = 0f;
                while (lerpValue < 1f)
                {
                    lerpValue += Time.deltaTime / FadeDuration;
                    _source.volume = Mathf.Lerp(_volume, 0f, lerpValue);
                    yield return null;
                }
            }
        }
        if (endFadeDuration > 0)
        {
            if (endFadeDuration > 0)
            {
                float lerpValue = 0f;
                while (lerpValue < 1f)
                {
                    lerpValue += Time.deltaTime / endFadeDuration;
                    _source.volume = Mathf.Lerp(_volume, 0f, lerpValue);
                    yield return null;
                }
            }
        }

        _source.Stop();
    }

    public void PlaySong(AudioClip song)
    {
        StartCoroutine(PlaySongE(song));
    }

    private IEnumerator PlaySongE(AudioClip clip)
    {
        _source.Stop();
        _source.clip = clip;
        _source.Play();
        StartCoroutine(FadeIn());
        while (_source.isPlaying)
        {
            if (_source.clip.length - _source.time <= FadeDuration)
            {
                yield return StartCoroutine(FadeOut());
            }
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        if (FadeDuration > 0f)
        {
            float startTime = _source.clip.length - FadeDuration;
            float lerpValue = 0f;
            while (lerpValue < 1f && _source.isPlaying)
            {
                lerpValue = Mathf.InverseLerp(startTime, _source.clip.length, _source.time);
                _source.volume = Mathf.Lerp(_volume, 0f, lerpValue);
                yield return null;
            }
            _source.volume = 0f;
        }
    }

    private IEnumerator FadeIn()
    {
        if (FadeDuration > 0f)
        {
            var lerpValue = 0f;
            while (lerpValue < 1f && _source.isPlaying)
            {
                lerpValue = Mathf.InverseLerp(0f, FadeDuration, _source.time);
                _source.volume = Mathf.Lerp(0f, _volume, lerpValue);
                yield return null;
            }
            _source.volume = _volume;
        }
    }

    public static List<T> Shuffle<T>(List<T> list)
    {
        return list.OrderBy(x => UnityEngine.Random.value).ToList();
    }
}