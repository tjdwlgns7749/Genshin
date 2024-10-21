using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
    public ItemData Data { get; private set; }
    public ItemOption Option { get; private set; }

    public int value { get; set; }//count,WeaponLevel
    public int optionvalue { get; set; }
    public int EXP { get; set; } = 0;
    public int MAXEXP { get; set; } = 0;

    public void ItemDataSetting(ItemData data, ItemOption option, Dictionary<int, int> weaponDatas)
    {
        Data = data;
        Option = option;
        value = 0;
        optionvalue = option.Value;
        EXP = 0;
        MAXEXP = weaponDatas[1];
    }

    public abstract void Use(Player player,Weapon weapon = null);
}
