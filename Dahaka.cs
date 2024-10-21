using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dahaka", menuName = "Scriptable Object Asset/Dahaka")]
public class Dahaka : MonsterAttack
{
    public override int Attack(bool[] skillDatas)
    {
        int select = 0;

        for (int i = skillDatas.Length - 1; i >= 0; i--)
        {
            if (skillDatas[i])//스킬사용가능
            {
                select = i;
                return i + 1;
            }
        }
        return 1;
    }
}
