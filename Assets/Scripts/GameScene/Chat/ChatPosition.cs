using System;
using UnityEngine;

public class ChatPosition : MonoBehaviour
{
    public Action<GameObject> PositionChatEventHandler;
    private RectTransform thisChat;

    private void Awake()
    {
        thisChat = GetComponent<RectTransform>();
    }

    void Start()
    {
        thisChat.localPosition = new Vector3(740, -259, 45.5968933f);
    }

    public void PositionNow(GameObject scene)
    {
        if (scene.name == "MainMenu")
        {
            thisChat.localPosition = new Vector3(740, -259, 45.5968933f);
        }
        if (scene.name == "ChoiceModeMenu")
        {
            thisChat.localPosition = new Vector3(731, -267, 45.5968933f);
        }
        if (scene.name == "TwitchMenu")
        {
            thisChat.localPosition = new Vector3(-744, -252, 45.5968933f);
        }
        if (scene.name == "SimpleGame")
        {
            thisChat.localPosition = new Vector3(0, -255, 45.5968933f);
        }
        if (scene.name == "CreateMode")
        {
            thisChat.localPosition = new Vector3(0, -255, 45.5968933f);
        }
        if (scene.name == "FinishGame")
        {
            thisChat.localPosition = new Vector3(0, -255, 45.5968933f);
        }
    }
}
