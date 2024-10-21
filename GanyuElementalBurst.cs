using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanyuElementalBurst : MonoBehaviour
{
    public HitCheck hitCheck;
    public GameObject Effect;
    SkillData skillData;
    float Damage = 0;

    public void useSkill(float player_damage, SkillData skilldata)
    {
        Damage = player_damage + skilldata.Damage;
        skillData = skilldata;
    }

    public void EffectActive(bool Active)
    {
        Effect.SetActive(Active);
        if (Active)
        {
            gameObject.transform.parent = null;
            StartCoroutine(SkillEnd());
        }
    }

    IEnumerator SkillEnd()
    {
        StartCoroutine(HitCheck());
        yield return new WaitForSeconds(10.0f);
        EffectActive(false);
        gameObject.transform.parent = GameObject.Find("PlayerManager").transform.Find("Avatar_Girl_Bow_Ganyu");
        gameObject.transform.localPosition = new Vector3(0, 0, 5);
        yield return null;
    }

    IEnumerator HitCheck()
    {
        for(int i=0; i < 10; i++)
        {
            hitCheck.AttackColliderCheck(Damage, skillData);
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
    }
}
