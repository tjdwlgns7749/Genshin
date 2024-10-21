using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public override void Use(Player player, Weapon weapon = null)
    {
        switch(Option.Option)
        {
            case itemOption.Heal:
                if (player.HP < player.MAXHP)
                {
                    player.Heal(optionvalue);
                    value--;
                }
                    break;
                
        }
    }
}
