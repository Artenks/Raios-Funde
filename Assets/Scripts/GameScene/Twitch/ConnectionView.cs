using TMPro;
using UnityEngine;

public class ConnectionView : MonoBehaviour
{

    public ConnectOnTwitch ConnectOnTwitch;
    public TMP_Text _connectionText;
    public TMP_Text _chatText;

    private TMP_Text _connectionIconText;
    void Start()
    {
        _connectionIconText = GetComponent<TMP_Text>();

        ConnectOnTwitch_ConnectionEventHandler(false);
        ConnectOnTwitch.ConnectionHandler += ConnectOnTwitch_ConnectionEventHandler;
    }

    private void ConnectOnTwitch_ConnectionEventHandler(bool isConnected)
    {
        if (isConnected)
        {
            _connectionText.text = "Conectado";
            _connectionText.color = new Color32(255, 214, 0, 255);

            _connectionIconText.text = "●";
            _connectionIconText.color = new Color32(255, 214, 0, 255);

            _chatText.color = new Color32(255, 214, 0, 255);
        }
        else
        {
            _connectionText.text = "Desconectado";
            _connectionText.color = new Color32(255, 0, 86, 255);

            _connectionIconText.text = "■";
            _connectionIconText.color = new Color32(255, 0, 86, 255);

            _chatText.color = new Color32(255, 0, 86, 255);
        }
    }
}
