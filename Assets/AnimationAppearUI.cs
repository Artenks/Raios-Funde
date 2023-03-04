using UnityEngine;

public class AnimationAppearUI : MonoBehaviour
{
    public GameObject UIToAppear;
    public bool State;
    private void OnEnable()
    {
        UIToAppear.SetActive(State);
    }
}
