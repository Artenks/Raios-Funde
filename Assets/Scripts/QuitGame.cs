using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnEnable()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
}
