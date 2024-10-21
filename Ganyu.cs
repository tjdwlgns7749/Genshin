using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using JetBrains.Annotations;

public class Ganyu : Player
{
    GanyuElementalSkill elementalSkill;
    GanyuElementalBurst burstSkill;
    public UseArrow useArrow;
    public Transform ArrowStartPos;
    public SkillData[] skilldatas;

    private new void  Start()
    {
        base.Start();

        elementalSkill = FindAnyObjectByType<GanyuElementalSkill>();
        burstSkill = FindAnyObjectByType<GanyuElementalBurst>();
    }

    public void BowSFX()
    {
        PlaySFX("BowAttack");
    }

    public void EskillSFX()
    {
        PlaySFX("GanyuEvoice");
    }

    public void BskillSFX()
    {
        PlaySFX("GanyuQvoice");
    }

    public void PlaySFX(string soundName)
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (audioClips[i].name == soundName)
            {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                return;
            }
        }
    }

    void asdElementalSkillEffect()
    {
        elementalSkill.EffectActive(true);
    }

    void ElementalBurstEffect()
    {
        burstSkill.EffectActive(true);
    }

    void getArrow()
    {
        useArrow.GetArrow(ArrowStartPos.transform.position, EquitWeapon.optionvalue, skilldatas[0], transform.forward, useArrow);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.GameMode)
        {
            float value = context.ReadValue<System.Single>();
            if (context.performed)
            {
                if (context.interaction is HoldInteraction)
                {
                    
                }
                else if (context.interaction is PressInteraction)
                {
                    if (playerAnimation.FirstAttack && playerState == Player_State.Idle)
                    {
                        playerAnimation.PlayTrigger("FirstAttack");
                        playerState = Player_State.Attack;
                        playerAnimation.FirstAttack = false;
                        handWeapon.gameObject.SetActive(true);
                        idleWeapon.gameObject.SetActive(false);
                    }
                    else
                        playerAnimation.ComboAttack();
                }
            }
        }
    }

    public void ElementalSkill(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<System.Single>();
        if (context.started)
        {
            playerAnimation.PlayTrigger("ElementalSkill");
            elementalSkill.useSkill(EquitWeapon.optionvalue, skilldatas[1]);
 
        }
    }

    public void ElementalBurst(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<System.Single>();
        if (context.started)
        {
            playerAnimation.PlayTrigger("ElementalBurst");
            burstSkill.useSkill(EquitWeapon.optionvalue, skilldatas[2]);
        }
    }
}
