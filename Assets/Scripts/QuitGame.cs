using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnEnable()
    {
        Application.Quit();
    }
}
