using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    private TMP_InputField _inputField;

    public void SendLineMessage(string msg)
    {
        _inputField.text = "";

        _inputField.ActivateInputField();
    }
    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSubmit.AddListener(SendLineMessage);
    }

}
