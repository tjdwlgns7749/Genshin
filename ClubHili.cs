using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHili : Monster
{
    HiliAttack hiliAttack;

    private new void Start()
    {
        base.Start();
        hiliAttack = GetComponent<HiliAttack>();
    }

    public void useAttack()
    {
        hiliAttack.DamageSet(Damage, skillDatas[0]);
    }
}
