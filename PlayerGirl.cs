using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerGirl : Player
{
    PlayerAttack playerAttack;
    PlayerElementalSkill playerElementalSkill;
    PlayerElementalBurst playerElementalBurst;

    public GameObject elementalSkillEffect;

    public SkillData[] skilldatas;

    private new void Start()
    {
        base.Start();

        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        playerElementalSkill = GetComponent<PlayerElementalSkill>();
        playerElementalBurst = FindAnyObjectByType<PlayerElementalBurst>();
    }

    public void SwordSFX()
    {
        PlaySFX("SwordAttack");
    }

    public void EskillSFX()
    {
        PlaySFX("playerEvoice");
    }

    public void BskillSFX()
    {
        PlaySFX("playerQvoice");
    }

    public void PlaySFX(string soundName)
    {
        for(int i =0;i<audioClips.Length;i++)
        {
            if (audioClips[i].name == soundName)
            {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                return;
            }
        }
    }

    void ElementalSkillEffect(int Active)
    {
        if (Active == 0)
            elementalSkillEffect.SetActive(false);
        else
            elementalSkillEffect.SetActive(true);
    }

    void ElementalBurstEffect()
    {
        playerElementalBurst.EffectActive(true);
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
                    Debug.Log("°­°ø°Ý");
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

                    playerAttack.DamageSet(EquitWeapon.optionvalue, skilldatas[0]);
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
            playerAnimation.ElementalSkillCharge(true);
            playerElementalSkill.useSkill(EquitWeapon.optionvalue, skilldatas[1]);
        }
        else if (context.canceled)
        {
            playerAnimation.ElementalSkillCharge(false);
        }
    }

    public void ElementalBurst(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<System.Single>();
        if (context.started)
        {
            playerAnimation.PlayTrigger("ElementalBurst");
            playerElementalBurst.useSkill(EquitWeapon.optionvalue, skilldatas[2]);
        }
    }
}
