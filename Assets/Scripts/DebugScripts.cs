using TMPro;
using UnityEngine;

public class DebugScripts : MonoBehaviour
{
    [SerializeField] TMP_Text fpsText;
    [SerializeField] GameObject LogPanelObject;

    void Start()
    {
        InvokeRepeating(nameof(CalculateFps), 0, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown("f1"))
        {
            if (LogPanelObject.activeInHierarchy)
            {
                LogPanelObject.SetActive(false);

            }
            else
            {
                LogPanelObject.SetActive(true);
            }
        }
    }

    private void CalculateFps()
    {
        if (LogPanelObject.activeInHierarchy)
        {
            var fps = 1f / Time.deltaTime;
            fpsText.text = $"{fps.ToString("00")} Fps";
        }

    }

}
