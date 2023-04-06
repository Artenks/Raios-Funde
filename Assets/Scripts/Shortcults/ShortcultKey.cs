using TMPro;
using UnityEngine;

public class ShortcultKey : MonoBehaviour
{
    public TMP_InputField InputFieldToSelect;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputFieldToSelect.Select();
        }
    }
}
