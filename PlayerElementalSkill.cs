using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalSkill : MonoBehaviour
{
    public HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;

    public void useSkill(float player_damage, SkillData skilldata)
    {
        Damage = player_damage + skilldata.Damage;
        skillData = skilldata;
    }

    public void elementalSkillAttackCheck()
    {
        hitCheck.AttackColliderCheck(Damage, skillData);
    }
}
