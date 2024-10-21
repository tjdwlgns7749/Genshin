using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Breath : MonoBehaviour
{
    BoxCollider boxCollider;
    HitCheck hitCheck;
    SkillData skillData;
    float Damage = 0;

    private void Start()
    {
        hitCheck = GetComponent<HitCheck>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void useBreath(float MonsterDamage, SkillData skilldata)
    {
        Damage = MonsterDamage + skilldata.Damage;
        skillData = skilldata;
    }

    public void breathCollider(bool check)
    {
        boxCollider.enabled = check;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hitCheck.AttackColliderCheck(Damage, skillData);
        }
    }
}