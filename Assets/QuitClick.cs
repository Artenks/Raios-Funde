using UnityEngine;

public class QuitClick : MonoBehaviour
{
    public Animator Animator;
    public void WalkingQuit()
    {
        Animator.SetTrigger("QuitGame");
    }
}
