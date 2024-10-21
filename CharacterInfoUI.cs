using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterInfoUI : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterLevel;
    public TextMeshProUGUI characterMaxHP;
    public TextMeshProUGUI characterDamage;
    public TextMeshProUGUI maxHPValue;
    public TextMeshProUGUI damageValue;

    public void SetInfo(string name,int level,float maxHP,float damage)
    {
        characterName.text = name;
        characterLevel.text = level.ToString();
        maxHPValue.text = maxHP.ToString();
        damageValue.text = damage.ToString();
    }
}
