using UnityEngine;

public abstract class MonsterAttack : ScriptableObject
{
    public abstract int Attack(bool[] skilldata);

}