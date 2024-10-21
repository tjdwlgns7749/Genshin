using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public bool gizmo = false;
    public Vector3 AttackColliderSize = new Vector3(0.5f, 0.5f, 0.5f);
    public LayerMask targetLayer;
    SortedSet<int> ints = new SortedSet<int>();

    public void AttackColliderCheck(float Damage, SkillData skillData)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, skillData.CheckSize / 2, Quaternion.identity, targetLayer);
        if (colliders.Length != 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
                {
                    var monster = colliders[i].GetComponent<Monster>();
                    if (monster.elemental_State == Elemental_State.Fire || monster.elemental_State == Elemental_State.Ice)
                        ints.Add((int)monster.elemental_State);
                }
            }
            for (int i = 0; i < colliders.Length; i++)//범위 내 검색
            {
                colliders[i].GetComponent<Damageable>().SkillElementalCheck(Damage, skillData.Elemental, ints);
            }
            ints.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (gizmo)
            Gizmos.DrawWireCube(transform.position, AttackColliderSize);
    }
}
