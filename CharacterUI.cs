using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    //���߿� public �ٲٱ�
    public GameObject Content;
    public CharacterInfoUI characterInfo;
    public WeaponList weaponList;

    public GameObject mainUI;
    public GameObject weaponUI;
    public LevelUI levelUI;

    public Player selectPlayer;

    Item item;

    public CharacterSlot[] characterSlots;
    [SerializeField] int MaxSlot = 2;
    public int SelectNum { get; set; } = 0;

    private void Awake()
    {
        characterSlots = new CharacterSlot[MaxSlot];
        for(int i =0; i<MaxSlot;i++)
        {
            characterSlots[i] = Content.transform.GetChild(i).GetComponent<CharacterSlot>();
        }
        
    }

    private void OnEnable()
    {
        //ĳ���� ����Ʈ ���� �� �׸���
        ShowCharacter();
        InfoUpdate(SelectNum);
        selectPlayer = characterSlots[SelectNum].player;

        //�𵨸� �ʱ�ȭ
        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterSlots[i].model.gameObject.SetActive(false);
        }

        //������ ĳ���� �𵨸� �ѱ�
        characterSlots[SelectNum].model.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SelectNum = 0;
    }

    public void ButtonClick(bool Active)
    {
        //������ư ������ �����ִϸ��̼�
        characterSlots[SelectNum].uiCharacter.standToWeapon(Active);
    }

    public void WeaponButtonClick(bool Active)
    {
        //�ִϸ��̼� ��ȯ
        characterSlots[SelectNum].uiCharacter.standToWeapon(Active);

        //UI ��ȯ
        mainUI.SetActive(!Active);//��ư ĳ���� ����Ʈ close��ư
        characterInfo.gameObject.SetActive(!Active);//����
        weaponUI.SetActive(Active);//����â

        //�������� ����
        characterSlots[SelectNum].uiCharacter.EquitWeapon(characterSlots[SelectNum].player.EquitWeapon.Data.Name, Active);
        InfoUpdate(SelectNum);
    }

    public void SlotClick(int index)
    {
        //�ִϸ��̼� ��ȯ
        characterSlots[SelectNum].uiCharacter.standToWeapon(false);

        //�𵨸� ��ȯ
        characterSlots[SelectNum].model.gameObject.SetActive(false);
        characterSlots[index].model.gameObject.SetActive(true);

        //ĳ�������� ��ȯ
        SelectNum = index;
        InfoUpdate(SelectNum);

        selectPlayer = characterSlots[SelectNum].player;
    }

    public void WeaponChange(string name)
    {
        characterSlots[SelectNum].uiCharacter.WeaponChange(name);
    }

    public void InfoUpdate(int index)
    {
        (string name, int Lv, float MaxHP, float Damage) InfoData = PlayerManager.Instance.InfoGetData(index);
        characterInfo.SetInfo(InfoData.name, InfoData.Lv, InfoData.MaxHP, InfoData.Damage);
    }
    public void ShowCharacter()
    {
        (int index, Sprite face, GameObject model, WeaponType type, Player player)[] SlotData = PlayerManager.Instance.CharacterSlotData();

        for (int i = 0; i < SlotData.Length; i++)
        {
            characterSlots[i].gameObject.SetActive(true);
            characterSlots[i].DataSetting(SlotData[i].index, SlotData[i].face, SlotData[i].model, SlotData[i].type, SlotData[i].player);
        }
    }

    public void LevelUpButton(bool Active)
    {
        if (Active)
        {
            levelUI.gameObject.SetActive(Active);
            item = InventoryManager.Instance.getItemData(7);
            if (item != null)
                levelUI.textSetting(selectPlayer.Level, selectPlayer.EXP, selectPlayer.MAXEXP, item.Data.Icon, item.value);
            else
                levelUI.textSetting(selectPlayer.Level, selectPlayer.EXP, selectPlayer.MAXEXP, null, 0);
        }
        else
            levelUI.gameObject.SetActive(Active);
    }

    public void LevelUpUseButton()
    {
        InventoryManager.Instance.UseLevelUpButton(selectPlayer);
        LevelUpButton(true);
        InfoUpdate(SelectNum);
    }

    
}
