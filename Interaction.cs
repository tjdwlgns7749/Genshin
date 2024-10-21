using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    NPC,
    SpawnObj
}

public interface Interaction
{
    public InteractionType Type { get; set; }
    public void ClickInteraction();
    public void SelectButton();

}
