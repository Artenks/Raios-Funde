using UnityEngine;

public class BackgroundAnimationMove : MonoBehaviour
{
    public Animator Animator;
    public bool NotOnDisable;

    public GameObject FinishScene;
    public GameObject GameScene;

    //private void OnDisable()
    //{
    //    if (NotOnDisable)
    //        return;


    //}

    //public void OnEnable()
    //{

    //}

    private void Update()
    {
        if (!FinishScene.activeInHierarchy && !GameScene.activeInHierarchy && Animator.GetBool("GoToSide"))
        {
            Animator.SetBool("GoToSide", false);
            Animator.SetBool("GoToMiddle", true);
        }
        if (FinishScene.activeInHierarchy && !Animator.GetBool("GoToSide") || GameScene.activeInHierarchy && !Animator.GetBool("GoToSide"))
        {
            Animator.SetBool("GoToSide", true);
            Animator.SetBool("GoToMiddle", false);
        }
    }
}
