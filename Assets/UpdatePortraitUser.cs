using UnityEngine;
using UnityEngine.UI;

public class UpdatePortraitUser : MonoBehaviour
{
    public ChatMessageLoad ChatMessageLoad;

    void Awake()
    {
        ChatMessageLoad.PortraitEventHandler += ChatMessageLoad_PortraitEventHandler;
    }

    private void ChatMessageLoad_PortraitEventHandler(string portrait)
    {
        TakePortrait(portrait);
    }

    private void TakePortrait(string portrait)
    {
        var portraits = this.GetComponentsInChildren<Image>(true);

        foreach (var item in portraits)
        {
            if (item.name == "Broadcaster")
            {
                if (portrait == "Broadcaster")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }

            if (item.name == "BroadcasterC")
            {
                if (portrait == "BroadcasterC")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }

            if (item.name == "Mod")
            {
                if (portrait == "Mod")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }

            if (item.name == "Vip")
            {
                if (portrait == "Vip")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }

            if (item.name == "Subscriber")
            {
                if (portrait == "Subscriber")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }

            if (item.name == "Pleb")
            {
                if (portrait == "Pleb")
                {
                    item.gameObject.SetActive(true);
                    return;
                }
                item.gameObject.SetActive(false);
                continue;
            }
        }
    }
}

