using UnityEngine;

public class CameraStartMove : MonoBehaviour
{
    public Camera PerspectiveCamera;
    public Animator Animator;

    public void CameraStarting()
    {
        PerspectiveCamera.gameObject.SetActive(true);
        Animator.SetTrigger("StartMove");
    }
}
