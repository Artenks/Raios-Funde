using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockModes : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;

    private TMP_Text text;
    private Button button;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();

        ConnectOnTwitch_ConnectionEventHandler(false);

        ConnectOnTwitch.ConnectionHandler += ConnectOnTwitch_ConnectionEventHandler;
    }

    private void ConnectOnTwitch_ConnectionEventHandler(bool isConnected)
    {
        if (isConnected)
        {
            text.isRightToLeftText = false;
            button.interactable = true;
        }
        else
        {
            text.isRightToLeftText = true;
            button.interactable = false;
        }
    }
}
