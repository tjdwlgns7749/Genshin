using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    PlayerAttack playerAttack;
    Player player;

    public bool AttackTimingCheck { get; set; } = false;
    public bool ComboAttackCheck { get; set; } = false;
    public bool FirstAttack { get; set; } = true;

    float MoveSpeed = 0.0f;
    bool Move = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
    }

    public void Updates()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        if (Move)
            MoveSpeed += 1.5f * Time.deltaTime;
        else
            MoveSpeed -= 1.5f * Time.deltaTime;

        MoveSpeed = Mathf.Clamp01(MoveSpeed);
        anim.SetFloat("MoveSpeed", MoveSpeed);
    }

    public void ComboAttackAnimation()//ÄÞº¸ °ø°Ý
    {
        if (ComboAttackCheck)
            anim.SetTrigger("NextAttack");
        else
        {
            FirstAttack = true;
            player.playerState = Player_State.Idle;
        }
        ComboAttackCheck = false;
    }

    public void MoveAnimation(bool Active)
    {
        Move = Active;
        anim.SetBool("isMove", Active);
        if(Active)
        {
            anim.SetFloat("isRunning", 1.0f);
        }
        else
        {
            anim.SetFloat("isRunning", 0.0f);
        }
    }

    public void SprintAnimation(bool Active)
    {
        anim.SetBool("isSprint", Active);
    }

    //AttackExit - AttackExit
    //FallToGroundL - Standby
    public void PlayTrigger(string AniName)
    {
        if (anim)
            anim.SetTrigger(AniName);
    }

    public void ElementalSkillCharge(bool Active)
    {
        anim.SetBool("ElementalSkillCharge", Active);
    }

    public void AttackToSprint()
    {
        FirstAttack = true;
        ComboAttackCheck = false;
        AttackTimingCheck = false;
    }

    public void AttackTimingStart()
    {
        AttackTimingCheck = true;
    }

    public void AttackTimingExit()
    {
        AttackTimingCheck = false;
    }

    public void ComboAttack()
    {
        if (AttackTimingCheck)
            ComboAttackCheck = true;
    }
}
