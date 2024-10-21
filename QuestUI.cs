using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;
    public QuestItemSlot[] images;

    public GameObject acceptButton;
    public GameObject clearButton;

    int questId;
    QuestData questdata;

    public void SetData(int id)
    {
        QuestData questData = QuestManager.Instance.GetQuestData(id);

        questdata = questData;
        questId = questData.ID;
        titleText.text = questData.Title;
        infoText.text = questData.Contents;

        for(int i =0;i<images.Length;i++)//초기화
        {
            images[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < questData.Item.Length; i++)//보상이미지
        {
            images[i].gameObject.SetActive(true);
            images[i].Setting(ItemManager.Instance.getItemIcon(questData.Item[i]), questData.ItemValue[i]);
            
        }

        if(questData.State == Quest_State.None)
        {
            acceptButton.SetActive(false);
            clearButton.SetActive(false);

            acceptButton.SetActive(true);
        }
        else if (questData.State == Quest_State.Clear)
        {
            acceptButton.SetActive(false);
            clearButton.SetActive(false);

            clearButton.SetActive(true);
        }
    }

    public void AcceptButton()
    {
        QuestManager.Instance.AcceptQuest(questId);
        GameManager.Instance.QuestAcceptButtonClick();
    }

    public void ClearButton()
    {
        QuestManager.Instance.QuestClear(questId);
        GameManager.Instance.QuestRefuseButtonClick();
    }

    public void RefuseButton()
    {
        GameManager.Instance.QuestRefuseButtonClick();
    }
}
