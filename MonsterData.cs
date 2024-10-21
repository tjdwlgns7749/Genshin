using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData" , menuName = "Scriptable Object Asset/MonsterData")]
public class MonsterData : ScriptableObject
{
    [SerializeField] string monsterName;
    [SerializeField] int level;
    [SerializeField] int maxHP;
    [SerializeField] float damage;

    public string MonsterName => monsterName;
    public int Level => level;
    public int MAXHP => maxHP;
    public float Damage => damage;
}
