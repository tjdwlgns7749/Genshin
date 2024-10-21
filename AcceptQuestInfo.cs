using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcceptQuestInfo : MonoBehaviour
{
    public TextMeshProUGUI title_text;
    public TextMeshProUGUI etc_text;
    public TextMeshProUGUI target_text;
    public QuestItemSlot[] quest_ItemSlots;

    public void DataSetting(QuestData data)
    {
        if (data != null)
        {
            title_text.text = data.Title;
            etc_text.text = data.Contents;
            target_text.text = data.Value.ToString() + " / " + data.MaxValue.ToString();

            for (int i = 0; i < quest_ItemSlots.Length; i++)
            {
                quest_ItemSlots[i].gameObject.SetActive(true);
                quest_ItemSlots[i].Setting(ItemManager.Instance.getItemIcon(data.Item[i]), data.ItemValue[i]);
            }
        }
        else
        {
            title_text.text = "";
            etc_text.text = "";
            target_text.text = "";

            for(int i =0;i<quest_ItemSlots.Length;i++)
            {
                quest_ItemSlots[i].gameObject.SetActive(false);
            }

        }
    }

}
