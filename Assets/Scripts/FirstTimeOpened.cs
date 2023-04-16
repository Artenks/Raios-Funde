using UnityEngine;

public class FirstTimeOpened : MonoBehaviour
{
    public GameObject WhenThisObjectDisappear;

    public Animator Animator;
    public GameObject PaperContent;
    private GameObject _initialMusic;

    private bool IsStoping;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.DeleteKey("FirstTime");
        }

        _initialMusic = GameObject.FindGameObjectWithTag("InitialMusic");
        if (Animator == null || PaperContent == null)
        {
            if (PlayerPrefs.GetInt("FirstTimeInGame") == 0)
                SetValueTrigger();
            else
            {
                Destroy(_initialMusic);
            }
            return;
        }

        try
        {
            if (PlayerPrefs.GetInt("FirstTimeInGame") == 1)
            {
                PaperContent.gameObject.SetActive(false);
                Animator.SetTrigger("StartMove");
            }

        }
        catch
        {
            PlayerPrefs.SetInt("FirstTimeInGame", 0);
        }
    }

    private void Update()
    {
        if (_initialMusic != null)
        {

            if (IsStoping)
            {
                if (!_initialMusic.GetComponent<AudioSource>().isPlaying)
                {
                    Destroy(_initialMusic);
                    IsStoping = false;
                }
            }
            else
            {
                if (WhenThisObjectDisappear != null && !WhenThisObjectDisappear.activeInHierarchy)
                {
                    StopInitialMusic();
                }
            }
        }

    }

    public void SetValueTrigger()
    {
        PlayerPrefs.SetInt("FirstTimeInGame", 1);
    }

    public void StopInitialMusic()
    {
        _initialMusic.GetComponent<SoundsManager>().Stop(true, 1.5f);
        IsStoping = true;
    }
}
