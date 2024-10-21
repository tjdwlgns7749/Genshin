using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;

    public void usedash(float monsterDamage, SkillData skilldata)
    {
        skillData = skilldata;
        Damage = monsterDamage + skilldata.Damage;
    }

    public void AttackCheck2()
    {
        hitCheck.AttackColliderCheck(Damage, skillData);
    }
}
