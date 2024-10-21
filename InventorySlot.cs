using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public Image image;
    public TextMeshProUGUI valueText;
    int itemIndex = 0;

    public void DataSetting(int index,Sprite icon,int value)
    {
        image.sprite = icon;
        valueText.text = value.ToString();
        itemIndex = index; 
    }

    public void ButtonClicked()
    {
        inventoryUI.SelectNum = itemIndex;
        inventoryUI.SlotClick();
    }
       
}
