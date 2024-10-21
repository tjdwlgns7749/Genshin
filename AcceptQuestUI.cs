using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptQuestUI : MonoBehaviour
{
    public QuestSlot[] questSlots;
    public AcceptQuestInfo questInfo;

    private void OnEnable()
    {
        QuestShow();
        ShowInfo(null);
    }

    public void _ButtonClick(QuestData data)
    {
        ShowInfo(data);
    }

    public void ShowInfo(QuestData data)
    {
        questInfo.DataSetting(data);
    }

    public void QuestShow()
    {
        List<QuestData> questDatas = new List<QuestData>();

        questDatas = QuestManager.Instance.GetAccepQuestData();

        for(int i =0;i<questSlots.Length;i++)
        {
            questSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < questDatas.Count; i++)
        {
            questSlots[i].gameObject.SetActive(true);
            questSlots[i].DataSetting(questDatas[i]);
        }
    }
}
