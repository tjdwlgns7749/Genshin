using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    No,
    Weapon,//����
    Consumable,//�Ҹ������
    Etc,
    Gold
}
public enum itemOption
{
    No,
    Attack,//���⿡ ����� ���ݷ�
    Heal,//HPȸ���ϴ� �����ۿ� �� ȸ��
    EXP,//����ġ ������ ����� ������
    Gold,
    Sell,
    Enhancer
}

public enum WeaponType
{
    NotWeapon,
    Sword,
    Bow
}

public class ItemData 
{
    public int ID { get; set; }
    public string Name { get; set; }
    public itemType Type { get; set; }
    public int Option { get; set; }
    public Sprite Image { get; set; }
    public Sprite Icon { get; set; }
    public int MaxCount { get; set; }
    public string korName { get; set; }


    public ItemData(int id,string name, itemType type ,int option,Sprite sprite,Sprite icon,int maxCount,string kor)
    {
        ID = id;
        Name = name;
        Type = type;
        Option = option;
        Image = sprite;
        Icon = icon;
        MaxCount = maxCount;
        korName = kor;
    }
}
