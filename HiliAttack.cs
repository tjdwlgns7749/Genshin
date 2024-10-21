using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiliAttack : MonoBehaviour
{
    public HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;

    public void DamageSet(float monsterDamage, SkillData skilldata)
    {
        skillData = skilldata;
        Damage = monsterDamage + skilldata.Damage;
    }
    public void AttackCheck()
    {
        hitCheck.AttackColliderCheck(Damage, skillData);
    }
}
