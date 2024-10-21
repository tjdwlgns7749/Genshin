using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public QuestListButton[] buttons; 

    public void SetData(int[] ints)
    {
        for (int i = 0; i <= ints.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);

            if (i != ints.Length)
                buttons[i].ButtonSetting(QuestManager.Instance.getQuestTitle(ints[i]), ints[i]);
        }
    }
}
