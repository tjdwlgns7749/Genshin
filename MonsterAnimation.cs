using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayTrigger(string AniName)
    {
        anim.SetTrigger(AniName);
    }

    public void Velocity(float Speed)
    {
        anim.SetFloat("Speed", Speed);
    }

    public void MoveAnimation(bool test)
    {
        anim.SetBool("isMove", test);
    }
}
