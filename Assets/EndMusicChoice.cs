using System.Collections;
using UnityEngine;

public class EndMusicChoice : MonoBehaviour
{
    public float TimeToWait;

    public EndGameView EndGameView;

    public SoundsManager SoundsManager;

    public MusicPlaylist WinPlaylist;
    public MusicPlaylist LostPlaylist;

    private bool _startCoroutineTime;
    private bool _roundWinner;

    public void ChoiceAEndMusic(bool win)
    {
        SoundsManager.Stop(true, 1.5f);
        _roundWinner = win;
        _startCoroutineTime = true;
    }
    private void Update()
    {
        if (_startCoroutineTime)
            StartCoroutine(EndMusic(_roundWinner));

    }
    IEnumerator EndMusic(bool win)
    {
        _startCoroutineTime = false;
        if (win)
            yield return new WaitForSeconds(TimeToWait - 2.8f);
        else
            yield return new WaitForSeconds(TimeToWait);

        if (win)
        {
            SoundsManager.ChangePlaylist(WinPlaylist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.win;
        }
        else
        {
            SoundsManager.ChangePlaylist(LostPlaylist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.lost;
        }
    }
}
