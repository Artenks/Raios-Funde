using UnityEngine;

public class RedirectButton : MonoBehaviour
{
    public void FoundOAuthWeb()
    {
        Application.OpenURL("https://twitchapps.com/tmi/");
    }
}
