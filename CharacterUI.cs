using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    //나중에 public 바꾸기
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
        //캐릭터 리스트 셋팅 및 그리기
        ShowCharacter();
        InfoUpdate(SelectNum);
        selectPlayer = characterSlots[SelectNum].player;

        //모델링 초기화
        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterSlots[i].model.gameObject.SetActive(false);
        }

        //선택한 캐릭터 모델링 켜기
        characterSlots[SelectNum].model.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SelectNum = 0;
    }

    public void ButtonClick(bool Active)
    {
        //종류버튼 누르면 전투애니메이션
        characterSlots[SelectNum].uiCharacter.standToWeapon(Active);
    }

    public void WeaponButtonClick(bool Active)
    {
        //애니메이션 전환
        characterSlots[SelectNum].uiCharacter.standToWeapon(Active);

        //UI 전환
        mainUI.SetActive(!Active);//버튼 캐릭터 리스트 close버튼
        characterInfo.gameObject.SetActive(!Active);//인포
        weaponUI.SetActive(Active);//무기창

        //장착중인 무기
        characterSlots[SelectNum].uiCharacter.EquitWeapon(characterSlots[SelectNum].player.EquitWeapon.Data.Name, Active);
        InfoUpdate(SelectNum);
    }

    public void SlotClick(int index)
    {
        //애니메이션 전환
        characterSlots[SelectNum].uiCharacter.standToWeapon(false);

        //모델링 전환
        characterSlots[SelectNum].model.gameObject.SetActive(false);
        characterSlots[index].model.gameObject.SetActive(true);

        //캐릭터인포 전환
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
