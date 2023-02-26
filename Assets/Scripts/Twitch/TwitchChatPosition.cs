using System;
using UnityEngine;

public class TwitchChatPosition : MonoBehaviour
{
    public Action<GameObject> PositionChatEventHandler;
    public GameObject MainMenu;

    private RectTransform thisChat;

    void Start()
    {
        thisChat = GetComponent<RectTransform>();

        PositionNow(MainMenu);
    }

    public void PositionNow(GameObject scene)
    {
        if (scene.name == "MainMenu")
        {
            thisChat.localPosition = new Vector3(-630, -234.889999f, 45.5968933f);
        }
        if (scene.name == "ChoiceModeMenu")
        {
            thisChat.localPosition = new Vector3(-694.616699f, -277, 45.5968933f);
        }
        if (scene.name == "TwitchMenu")
        {
            thisChat.localPosition = new Vector3(-690.789978f, -1.52590001e-05f, 45.5968933f);
        }
        if (scene.name == "TogetherMenu")
        {
            thisChat.localPosition = new Vector3(0, -350, 45.5968933f);
        }
        if (scene.name == "ConfigMenu")
        {
            thisChat.localPosition = new Vector3(-630, -234.889999f, 45.5968933f);
        }
        if (scene.name == "CreditsMenu")
        {
            thisChat.localPosition = new Vector3(-694.616699f, -277, 45.5968933f);
        }
        if (scene.name == "FinishGame")
        {
            thisChat.localPosition = new Vector3(-0.00439453125f, -235.729996f, 45.5968933f);
        }
    }
}
