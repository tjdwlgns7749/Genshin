using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModeCharacter : MonoBehaviour
{
    Animator animator;
    public GameObject IdleWeapon;
    public GameObject HandWeapon;

    public GameObject[] handWeaponArry;
    public GameObject[] IdleWeaponArry;

    string EquitWeaponName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public void standToWeapon(bool Active)
    {
        animator.SetBool("Weapon", Active);
        IdleWeapon.SetActive(!Active);
        HandWeapon.SetActive(Active);
    }

    public void WeaponChange(string weaponName)
    {
        for(int i=0;i<handWeaponArry.Length;i++)
        {
            if (handWeaponArry[i].name == weaponName)
            {
                HandWeapon.SetActive(false);
                HandWeapon = handWeaponArry[i];
                HandWeapon.SetActive(true);
            }
        }
    }

    public void EquitWeapon(string weaponName, bool Active)
    {
        if(Active)//¹«±âÃ¢Å³¶© ÀÌ¸§ÀúÀå
        {
            EquitWeaponName = weaponName;
        }
        else//¹«±âÃ¢²ø¶© ³¢´ø³ð Ã£¾Æ¼­ ³¢¿ì±â
        {
            for(int i =0;i<handWeaponArry.Length;i++)
            {
                if (handWeaponArry[i].name == EquitWeaponName)
                {
                    HandWeapon.SetActive(false);
                    HandWeapon = handWeaponArry[i];
                    HandWeapon.SetActive(false);
                }
            }
        }
    }
}
