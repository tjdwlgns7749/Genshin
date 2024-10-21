using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlot : MonoBehaviour
{
    public QuestData quest_data;
    public TextMeshProUGUI title_text;
    public AcceptQuestUI acceptQuestUI;

    public void ButtonClick()
    {
        acceptQuestUI._ButtonClick(quest_data);
    }

    public void DataSetting(QuestData data)
    {
        quest_data = data;
        title_text.text = data.Title;
    }
}
