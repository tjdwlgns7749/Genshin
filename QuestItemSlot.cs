using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemSlot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI ItemValue;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Setting(Sprite sprite, int value)
    {
        image.sprite = sprite;
        ItemValue.text = value.ToString();
    }
}
