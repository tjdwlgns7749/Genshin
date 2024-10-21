using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object Asset/SkillData")]
public class SkillData : ScriptableObject
{
    public string Skill_Name;
    public int Skill_Level;
    public Vector3 CheckSize;
    public float Damage;
    public int Elemental;
    public float coolTime;
    public bool active;
}
