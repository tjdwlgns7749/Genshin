using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance = null;

    List<Item> inventoryList = new List<Item>();

    public static InventoryManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<InventoryManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
    }

    public Item FirstSetting(string weaponName)
    {
        for(int i=0;i<inventoryList.Count;i++)
        {
            if(inventoryList[i].Data.Name == weaponName)
            {
                return inventoryList[i];
            }
        }
        return null;
    }

    public bool UseItem(int selectItemIndex, Player player = null, Weapon weapon = null)
    {
        Player usePlayer = player;

        if (usePlayer == null)
            usePlayer = PlayerManager.Instance.getSelectCharacter();

        inventoryList[selectItemIndex].Use(usePlayer, weapon);

        if (inventoryList[selectItemIndex].value == 0)
        {
            inventoryList.Remove(inventoryList[selectItemIndex]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseWeapon(int selectItemIndex,Player player)
    {
        Debug.Log(selectItemIndex);
        inventoryList[selectItemIndex].Use(player);
    }

    public void AddItem(Item item)
    {
        if (!InventorySearch(item))
        {
            inventoryList.Add(item);
            item.value++;
        }
    }
    bool InventorySearch(Item item)
    {
        for(int i =0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Data.ID == item.Data.ID)//같은아이템 O
            {
                if (inventoryList[i].value +1 > inventoryList[i].Data.MaxCount)//초과
                {
                    return false;
                }
                else
                {
                    inventoryList[i].value++;
                    return true;
                }
            }
        }
        return false;
    }

    public (string,Sprite,string) ItemInfoGetData(int id)
    {
        return (inventoryList[id].Data.Name, inventoryList[id].Data.Image, inventoryList[id].Option.Etc);
    }

    public (int, int, Sprite, int)[] ItemSlotGetData(itemType type)
    {
        List<(int,int,Sprite,int)> slotDatas = new List<(int,int, Sprite, int)> ();

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Data.Type == type)
            {
                slotDatas.Add((inventoryList[i].Data.ID,i, inventoryList[i].Data.Icon, inventoryList[i].value));
            }
            
        }
        return slotDatas.ToArray();
    }

    public (string,int,int,string)WeaponInfoGetData(int id)
    {
        return (inventoryList[id].Data.korName, inventoryList[id].optionvalue, inventoryList[id].value, inventoryList[id].Option.Etc);
    }

    public Item WeaponInfoGetData2(int id)
    {
        return inventoryList[id];
    }
    public (int, int, Sprite, int, string)[] WeaponSlotGetData(WeaponType type)
    {
        List<(int, int, Sprite, int, string)> slotDatas = new List<(int, int, Sprite, int, string)>();

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Data.Type == itemType.Weapon)
            {
                if (inventoryList[i].Option.weaponType == type)
                {
                    slotDatas.Add((inventoryList[i].Data.ID, i, inventoryList[i].Data.Icon, inventoryList[i].value, inventoryList[i].Data.Name));
                }
            }
        }
        return slotDatas.ToArray();
    }

    public Item getItemData(int key)
    {
        for(int i=0;i<inventoryList.Count;i++)
        {
            if(inventoryList[i].Data.ID == key)
            {
                return inventoryList[i];
            }
        }
        return null;
    }

    public void UseLevelUpButton(Player player)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Data.ID == 7)
            {
                UseItem(i, player);
                return;
            }
        }
    }

    public void UseWeaponLevelUPButton(int select,Player player)
    {
        Weapon weapon = (Weapon)inventoryList[select];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].Data.ID == 8)
            {
                UseItem(i, player, weapon);
                return;
            }
        }
    }
}
