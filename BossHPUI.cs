using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class BossHPUI : MonoBehaviour
{
    public Sprite[] elemental_Sprites;
    public Image image;
    public TextMeshProUGUI boss_name;
    public Slider boss_HP;
    public TextMeshProUGUI boss_HP_text;

    public void Setting(string name,float hp , float maxhp, Elemental_State state)
    {
        boss_name.text = name;
        boss_HP.value = hp;
        boss_HP.maxValue = maxhp;
        boss_HP_text.text = hp.ToString() + " / " + maxhp.ToString();

        if (state == global::Elemental_State.Fire)
        {
            image.enabled = true;
            image.sprite = elemental_Sprites[0];
        }
        else if (state == global::Elemental_State.Ice)
        {
            image.enabled = true;
            image.sprite = elemental_Sprites[1];
        }
        else
        {
            image.enabled = false;
        }
    }
}
