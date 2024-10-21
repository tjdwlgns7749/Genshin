using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class categoryButton : MonoBehaviour,IPointerDownHandler
{
    public itemType type;
    public static event Action<itemType> OnCategoryButtonClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnCategoryButtonClick?.Invoke(type);
    }
}
