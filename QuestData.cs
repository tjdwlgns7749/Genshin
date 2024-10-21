using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quest_State
{
    None,
    Play,
    Clear,
    End
}
public class QuestData
{
    public int ID { get; set; }//퀘스트아이디
    public string Title { get; set; }//퀘스트제목
    public string Contents { get; set; }//퀘스트내용
    public int Value { get; set; }//퀘스트에 사용할 밸류
    public int MaxValue { get; set; }//퀘스트에 사용할 클리어 밸류
    public int EXP { get; set; }//클리어 경험치
    public int[] Item { get; set; }//퀘스트 보상 아이템 번호
    public int[] ItemValue { get; set; }//보상 아이템 갯수
    public Quest_State State { get; set; }//퀘스트 진행도
    public string TargetName { get; set; }//퀘스트 타겟이름

    public QuestData(int id,string title,string contents,int value,int maxValue,int exp, int[] item, int[] itemValue,string target)
    {
        ID = id;
        Title = title;
        Contents = contents;
        Value = value;
        MaxValue = maxValue;
        EXP = exp;
        Item = item;
        ItemValue = itemValue;
        State = Quest_State.None;
        TargetName = target;
    }
}
