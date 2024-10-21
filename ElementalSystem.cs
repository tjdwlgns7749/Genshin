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

    public float EvaporationCheck(float Damage, int SkillElemental, int MonsterElemental)//����
    {
        if((SkillElemental == (int)Elemental_State.Fire && MonsterElemental == (int)Elemental_State.Ice) || (SkillElemental == (int)Elemental_State.Ice && MonsterElemental == (int)Elemental_State.Fire))
        {
            gameObject.GetComponent<Monster>().elemental_State = Elemental_State.Normal;
            Damage *= 2.0f;

        }
        return Damage;
    }

    public void Diffusion(float Damage, SortedSet<int> ints, int MonsterElemental)//Ȯ��
    {
        foreach (var val in ints)
        {
            if (MonsterElemental == (int)Elemental_State.Normal)//���Ͱ� ���Ӽ�
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
        if (SkillElemental == (int)Elemental_State.Wind)//�ٶ���ų
        {
            Diffusion(Damage, ints, MonsterElemental);

        }
        else//�� ����
        {
            if (MonsterElemental == (int)Elemental_State.Normal)
                gameObject.GetComponent<Monster>().elemental_State = (Elemental_State)SkillElemental;
            else
                Damage = EvaporationCheck(Damage, SkillElemental, MonsterElemental);
        }
        return Damage;
    }
}
