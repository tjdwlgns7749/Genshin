using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class QuestListButton : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI title;
    int id = 0;

    public void ButtonSetting(string q_title,int q_id)
    {
        title.text = q_title;
        id = q_id;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (id != 0)
        {
            GameManager.Instance.QuestUI(true);
            GameManager.Instance.QuestUISetData(id);
        }
    }
}
