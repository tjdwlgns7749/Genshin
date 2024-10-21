using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    public void GetDamage(float Damage, int elementalnumber = 0);
    public void SkillElementalCheck(float Damage, int ElementalNumber, SortedSet<int> ints);
}