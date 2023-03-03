using UnityEngine;

public class SignatureView : MonoBehaviour
{
    public Animator Animator;
    public LineGenerator LineGenerator;
    void Awake()
    {
        LineGenerator.WrittedEventHandler += LineGenerator_WrittedEventHandler;
    }

    private void LineGenerator_WrittedEventHandler()
    {
        Animator.SetTrigger("AppearBox");
    }
}
