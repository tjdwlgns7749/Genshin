using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public TextMeshProUGUI _text;

    public InteractionButton interactionButton;

    public NPCSelectButton npcSelectButton;

    public void InteractionTarget(Interaction target)
    {
        if(target.Type == InteractionType.SpawnObj)
        {
            _text.text = "��ȯ";
        }
        else
        {
            _text.text = "��ȣ�ۿ�";
        }

        interactionButton.InteractionTarget(target);
        npcSelectButton.InteractionTarget(target);
    }

}
