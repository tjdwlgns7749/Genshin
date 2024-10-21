using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionButton : MonoBehaviour, IPointerDownHandler
{
    Interaction interaction_target;

    public void InteractionTarget(Interaction target)
    {
        interaction_target = target;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        interaction_target.ClickInteraction();
        GameManager.Instance.InteractionUI(false);
    }
}
