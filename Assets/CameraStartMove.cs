using UnityEngine;

public class CameraStartMove : MonoBehaviour
{
    public Camera PerspectiveCamera;
    public Camera OrtographicCamera;
    public Animator Animator;

    public void CameraStarting()
    {
        OrtographicCamera.gameObject.SetActive(false);
        PerspectiveCamera.gameObject.SetActive(true);

        Animator.SetTrigger("StartMove");
    }
}
