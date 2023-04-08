using UnityEngine;

public class SignatureMusicDisable : MonoBehaviour
{
    private SoundsManager _soundsManager;

    private void OnEnable()
    {
        _soundsManager = FindObjectOfType<SoundsManager>();
        if (PlayerPrefs.GetInt("FirstTime") == 1)
        {
            _soundsManager.Stop(true, 1.6f);
        }
    }
}
