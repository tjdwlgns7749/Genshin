using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI level_text;
    public Slider level_slider;
    public Image item_Image;
    public TextMeshProUGUI item_text;
    public TextMeshProUGUI EXP_text;

    public void textSetting(int level, int exp, int maxexp, Sprite sprite,int value)
    {
        if (sprite == null)
            item_Image.gameObject.SetActive(false);
        else
            item_Image.gameObject.SetActive(true);

        level_text.text = "Lv." + level.ToString();
        level_slider.value= exp;
        level_slider.maxValue = maxexp;
        item_Image.sprite = sprite;
        item_text.text = value.ToString();
        EXP_text.text = exp.ToString() + " / " + maxexp.ToString();

    }
}
