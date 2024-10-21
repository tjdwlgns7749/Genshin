using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    public HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;

    public void useEarthQuake(float monsterDamage, SkillData skilldata)
    {
        skillData = skilldata;
        Damage = monsterDamage + skilldata.Damage;
    }

    public void AttackCheck()
    {
        hitCheck.AttackColliderCheck(Damage, skillData);
    }

}
