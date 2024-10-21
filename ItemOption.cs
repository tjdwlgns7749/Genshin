using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOption
{
    public int ID { get; set; }
    public itemOption Option{ get; set; }
    public int Value { get; set; }
    public string Etc { get; set; }
    public WeaponType weaponType { get; set; }

    public ItemOption(int id,itemOption option,int value,string etc,WeaponType Type)
    {
        ID = id;
        Option = option;
        Value = value;
        Etc = etc;
        weaponType = Type;
    }
}
