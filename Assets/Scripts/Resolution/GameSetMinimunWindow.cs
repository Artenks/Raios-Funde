using UnityEngine;

public class GameSetMinimunWindow : MonoBehaviour
{
    void Awake()
    {
        MinimumWindowSize.Set(660, 500);
    }
    private void OnApplicationQuit()
    {
        MinimumWindowSize.Reset();
    }
}
