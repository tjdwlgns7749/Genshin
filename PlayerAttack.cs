using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;


    public void DamageSet(float playerDamage , SkillData attackSkillData)
    {
        Damage = playerDamage + attackSkillData.Damage;
        skillData = attackSkillData;
    }
    public void AttackCheck()
    {
        hitCheck.AttackColliderCheck(Damage, skillData);
    }
}
