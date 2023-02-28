using UnityEngine;

public class FinishReturn : MonoBehaviour
{
    public GameObject Game;
    public GameObject CreatePhrase;

    public GameManager GameManager;

    public void OnClickReturn()
    {
        if (GameManager.Data.PlayMode == GameManager.PlayModes.TogetherMode)
        {
            Game.SetActive(true);
            CreatePhrase.SetActive(false);

        }
        else
        {
            CreatePhrase.SetActive(true);
            Game.SetActive(false);
        }
    }
}
