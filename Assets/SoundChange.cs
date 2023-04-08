using UnityEngine;

public class SoundChange : MonoBehaviour
{
    public SoundsManager.SceneMusicTemplate MusicToGo;

    public SoundsManager SoundsManager;
    public MusicPlaylist Playlist;

    public void ChangingMusic(SoundsManager.SceneMusicTemplate soundToChange, MusicPlaylist playlist)
    {
        if (soundToChange == SoundsManager.SceneMusicTemplate.game && MusicToGo != SoundsManager.SceneMusic)
        {
            SoundsManager.ChangePlaylist(playlist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.game;
        }
        if (soundToChange == SoundsManager.SceneMusicTemplate.menu && MusicToGo != SoundsManager.SceneMusic)
        {
            SoundsManager.ChangePlaylist(playlist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.menu;
        }
        if (soundToChange == SoundsManager.SceneMusicTemplate.lost && MusicToGo != SoundsManager.SceneMusic)
        {
            SoundsManager.ChangePlaylist(playlist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.lost;
        }
        if (soundToChange == SoundsManager.SceneMusicTemplate.win && MusicToGo != SoundsManager.SceneMusic)
        {
            SoundsManager.ChangePlaylist(playlist, 0);
            SoundsManager.SceneMusic = SoundsManager.SceneMusicTemplate.win;
        }

    }

    private void OnEnable()
    {
        ChangingMusic(MusicToGo, Playlist);

    }
}
