using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Club", menuName = "Scriptable Object Asset/Club")]
public class Club : MonsterAttack 
{
    public override int Attack(bool[] skillDatas)
    {
        int select = 0;

        for(int i = skillDatas.Length - 1; i >= 0;i--)
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
