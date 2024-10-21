using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paimon : MonoBehaviour , Interaction
{
    [SerializeField] public InteractionType Type { get; set; } = InteractionType.NPC;


    Dictionary<int, string> talkDatas;
    public string NPC_name;

    [SerializeField]
    public int[] QuestID;
    public int questIndex = 1;

    

    public void ClickInteraction()
    {
        GameManager.Instance.TalkUI(true);
        GameManager.Instance.TalkUISetData(talkDatas, NPC_name);
    }

    public void SelectButton()
    {
        GameManager.Instance.QuestList(true);
        GameManager.Instance.QuestListSetData(QuestID);
    }

    public void TalkDataSettin(Dictionary<int,string> data)
    {
        talkDatas = data;
    }
}
