using UnityEngine;
using UnityEngine.Events;

public class TakeMessage : MonoBehaviour
{
    public UnityEvent<string, string> OnChatMessage;

    public void SendAMessage(string user, string message)
    {
        OnChatMessage?.Invoke(user, message);
    }
}
