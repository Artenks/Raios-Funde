using UnityEngine;
using UnityEngine.UI;

public class InfoAppearWhenClick : MonoBehaviour
{
    public GameObject Info;

    public void Appear()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
            Info.SetActive(true);

        else
            Info.SetActive(false);


        Debug.Log(this.gameObject.GetComponent<Toggle>().isOn);
    }
}
