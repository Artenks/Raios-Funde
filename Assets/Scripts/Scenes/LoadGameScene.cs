using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public int sceneToLoadIndex;
    public int sceneToUnloadIndex;
    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(sceneToLoadIndex);
        SceneManager.UnloadSceneAsync(sceneToUnloadIndex);
    }
}
