using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoUI : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemInfo;

    public void SetInfo(string name ,Sprite _image, string info)
    {
        itemName.text = name;
        image.sprite = _image;
        itemInfo.text = info;
    }
}
