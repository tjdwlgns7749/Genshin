using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;

public class ElementalSystem : MonoBehaviour
{
    Monster monster;

    private void Start()
    {
        monster = GetComponent<Monster>();
    }

    public float EvaporationCheck(float Damage, int SkillElemental, int MonsterElemental)//증발
    {
        if((SkillElemental == (int)Elemental_State.Fire && MonsterElemental == (int)Elemental_State.Ice) || (SkillElemental == (int)Elemental_State.Ice && MonsterElemental == (int)Elemental_State.Fire))
        {
            gameObject.GetComponent<Monster>().elemental_State = Elemental_State.Normal;
            Damage *= 2.0f;

        }
        return Damage;
    }

    public void Diffusion(float Damage, SortedSet<int> ints, int MonsterElemental)//확산
    {
        foreach (var val in ints)
        {
            if (MonsterElemental == (int)Elemental_State.Normal)//몬스터가 무속성
            {
                Damage *= 1.5f;
                monster.GetDamage(Damage);
                gameObject.GetComponent<Monster>().elemental_State = (Elemental_State)val;
                break;
            }
            else
            {
                EvaporationCheck(Damage, val, MonsterElemental);
            }
        }
    }

    public float ElementalCheck(float Damage, int SkillElemental, int MonsterElemental, SortedSet<int> ints)
    {
        if (SkillElemental == (int)Elemental_State.Wind)//바람스킬
        {
            Diffusion(Damage, ints, MonsterElemental);

        }
        else//불 얼음
        {
            if (MonsterElemental == (int)Elemental_State.Normal)
                gameObject.GetComponent<Monster>().elemental_State = (Elemental_State)SkillElemental;
            else
                Damage = EvaporationCheck(Damage, SkillElemental, MonsterElemental);
        }
        return Damage;
    }
}
