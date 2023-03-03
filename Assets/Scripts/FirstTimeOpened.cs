using UnityEngine;

public class FirstTimeOpened : MonoBehaviour
{
    public Animator Animator;
    public GameObject PaperContent;
    private void Awake()
    {
        try
        {
            if (PlayerPrefs.GetInt("FirstTime") == 1)
            {
                PaperContent.gameObject.SetActive(false);
                Animator.SetTrigger("StartMove");
            }
            Debug.Log("try");

        }
        catch
        {
            PlayerPrefs.SetInt("FirstTime", 0);
            Debug.Log("catch");
        }
    }

    public void SetValueTrigger()
    {
        PlayerPrefs.SetInt("FirstTime", 1);
    }
}
