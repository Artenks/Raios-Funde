using UnityEngine;
using UnityEngine.UI;

public class UnlockModes : MonoBehaviour
{
    public ConnectOnTwitch ConnectOnTwitch;

    private Button button;

    private void OnEnable()
    {

    }

    private void Awake()
    {
        button = GetComponent<Button>();

        ConnectOnTwitch_ConnectionEventHandler(false);

        ConnectOnTwitch.ConnectionHandler += ConnectOnTwitch_ConnectionEventHandler;
    }

    private void ConnectOnTwitch_ConnectionEventHandler(bool isConnected)
    {
        if (isConnected)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }


}
