using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    No,
    Weapon,//무기
    Consumable,//소모아이템
    Etc,
    Gold
}
public enum itemOption
{
    No,
    Attack,//무기에 사용할 공격력
    Heal,//HP회복하는 아이템에 쓸 회복
    EXP,//경험치 증가에 사용할 아이템
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
