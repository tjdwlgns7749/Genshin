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
    public int ID { get; set; }//����Ʈ���̵�
    public string Title { get; set; }//����Ʈ����
    public string Contents { get; set; }//����Ʈ����
    public int Value { get; set; }//����Ʈ�� ����� ���
    public int MaxValue { get; set; }//����Ʈ�� ����� Ŭ���� ���
    public int EXP { get; set; }//Ŭ���� ����ġ
    public int[] Item { get; set; }//����Ʈ ���� ������ ��ȣ
    public int[] ItemValue { get; set; }//���� ������ ����
    public Quest_State State { get; set; }//����Ʈ ���൵
    public string TargetName { get; set; }//����Ʈ Ÿ���̸�

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
