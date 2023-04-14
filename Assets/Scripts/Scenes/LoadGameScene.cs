using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public int sceneToLoadIndex;
    private void OnEnable()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
