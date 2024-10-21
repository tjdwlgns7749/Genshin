using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public ItemInfoUI itemInfoUI;
    public GoldUI goldUI;

    InventorySlot[] inventorySlots;
    public GameObject Content;

    public int SelectNum { get; set; } = 0;
    [SerializeField] int MaxSlot = 50;
    itemType type;
    [SerializeField] GameObject useButton;

    private void Awake()
    {
        inventorySlots = new InventorySlot[MaxSlot];
        type = itemType.Weapon;
        for (int i = 0; i < MaxSlot; i++)
        {
            inventorySlots[i] = Content.transform.GetChild(i).GetComponent<InventorySlot>();
        }
    }
    private void OnEnable()
    {
        categoryButton.OnCategoryButtonClick += HandleCategoryButtonClicked;
        type = itemType.Weapon;
        ShowInventory();
        useButtonUpdate(type);
    }

    private void OnDisable()
    {
        categoryButton.OnCategoryButtonClick -= HandleCategoryButtonClicked;
    }

    public void SlotClick()
    {
        InfoUpdate(SelectNum);
    }

    public void UseClick()
    {
        bool infoUpdate = InventoryManager.Instance.UseItem(SelectNum);
        ShowInventory(infoUpdate);
    }

    void useButtonUpdate(itemType type)
    {
        switch(type)
        {
            case itemType.Weapon:
            case itemType.Etc:
            case itemType.No:
                useButton.SetActive(false);
                break;
            case itemType.Consumable:
                useButton.SetActive(true);
                break;
        }
    }

    void InfoUpdate(int selectNum)
    {
        (string name, Sprite itemImage, string info) InfoData = InventoryManager.Instance.ItemInfoGetData(selectNum);
        itemInfoUI.SetInfo(InfoData.name, InfoData.itemImage, InfoData.info);
    }

    public void ShowInventory(bool Active = true)
    {
        (int id, int index, Sprite icon, int value)[] SlotData = InventoryManager.Instance.ItemSlotGetData(type);

        Array.Sort(SlotData, (x, y) => x.id.CompareTo(y.id));//정렬

        for(int i =0;i<MaxSlot;i++)
        {
            inventorySlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < SlotData.Length; i++)
        {
            inventorySlots[i].gameObject.SetActive(true);
            inventorySlots[i].DataSetting(SlotData[i].index, SlotData[i].icon, SlotData[i].value);
        }
        if (Active)
        {
            SelectNum = SlotData[0].index;//첫번째 슬롯이랑 info에 연결할수잇도록 재지정
            InfoUpdate(SelectNum);//info 업데이트하는 함수
            useButtonUpdate(type);
        }
    }

    private void HandleCategoryButtonClicked(itemType itemType)
    {
        for (int i = 0; i < MaxSlot; i++)
        {
            inventorySlots[i].gameObject.SetActive(false);
        }

        switch (itemType)
        {
            case itemType.Weapon:
                type = itemType.Weapon;
                ShowInventory();
                break;
            case itemType.Consumable:
                type = itemType.Consumable;
                ShowInventory();
                break;
            case itemType.Etc:
                type = itemType.Etc;
                ShowInventory();
                break;
        }
    }
}
