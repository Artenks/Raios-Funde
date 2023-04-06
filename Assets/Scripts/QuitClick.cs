using UnityEngine;

public class QuitClick : MonoBehaviour
{
    public Animator Animator;

    public GameObject EndTextObject;
    public Animator MenuAnimator;
    public Animator ChatAnimator;

    public void WalkingQuit()
    {
        ChatAnimator.SetTrigger("Exit");
        MenuAnimator.SetTrigger("Exit");
        EndTextObject.SetActive(true);

        Animator.SetInteger("Change", 2);
    }
}
