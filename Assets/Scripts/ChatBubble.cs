using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public static void Create(Transform parent, Vector3 localPosition, string text) 
    {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);
    }

    private SpriteRenderer background;
    private TMP_Text text;

    private void Awake()
    {
        background = GetComponentInChildren<SpriteRenderer>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void Setup(string _text) 
    {
        text.SetText(_text);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);

        Vector2 padding = new Vector2(1.5f, 0.75f);
        background.size = textSize + padding;
    }
}
