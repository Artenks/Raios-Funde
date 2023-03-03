using UnityEngine;

public class ActiveGameUI : MonoBehaviour
{
    public GameObject UIToActive;

    private void OnEnable()
    {
        UIToActive.SetActive(true);
    }

}
