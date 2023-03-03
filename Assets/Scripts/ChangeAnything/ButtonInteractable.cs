using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{
    public TMP_InputField InputField;
    private Button _buttonConfirm;
    void Awake()
    {
        _buttonConfirm = GetComponent<Button>();
    }

    private void Update()
    {
        if (InputField.text.Length > 0)
            _buttonConfirm.interactable = true;
        else
            _buttonConfirm.interactable = false;
    }
}
