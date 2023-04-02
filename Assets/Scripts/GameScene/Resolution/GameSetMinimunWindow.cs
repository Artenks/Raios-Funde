using UnityEngine;

public class GameSetMinimunWindow : MonoBehaviour
{
    void Awake()
    {
        MinimumWindowSize.Set(640, 480);
    }
    private void OnApplicationQuit()
    {
        MinimumWindowSize.Reset();
    }
}
