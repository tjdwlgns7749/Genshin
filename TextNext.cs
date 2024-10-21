using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextNext : MonoBehaviour, IPointerDownHandler
{
    public TalkUI talkUI;

    public void OnPointerDown(PointerEventData eventData)
    {
        talkUI.NextText();
    }
}
