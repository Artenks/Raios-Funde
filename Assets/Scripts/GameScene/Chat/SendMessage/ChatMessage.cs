using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    private ConnectTwitchMessager _connectTwitchMessager;
    private TMP_InputField _inputField;

    public void SendLineMessage(string msg)
    {
        _connectTwitchMessager.SendAMessage(msg);
        _inputField.text = "";
        Debug.Log("mensagem enviada");

        _inputField.ActivateInputField();
    }
    private void Awake()
    {
        _connectTwitchMessager = GameObject.FindGameObjectWithTag("Connection").GetComponent<ConnectTwitchMessager>();

        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSubmit.AddListener(SendLineMessage);
    }

}
