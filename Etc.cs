using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etc : Item
{
    public override void Use(Player player, Weapon weapon = null)
    {
        switch(Data.Name)
        {
            case "EXPBook":
                player.GetEXP(optionvalue);
                value--;
                break;
            case "Crystal":
                weapon.WeaponGetEXP(optionvalue);
                value--;
                break;
        }
    }
}
