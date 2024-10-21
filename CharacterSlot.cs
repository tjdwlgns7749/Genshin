using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    int characterIndex = 0;
    public Image image;
    public GameObject model;
    public WeaponType weapontype;
    public Player player;

    public CharacterUI characterUI;
    public UIModeCharacter uiCharacter;
    
    public void DataSetting(int index,Sprite face,GameObject modelobj,WeaponType type,Player _player)
    {
        characterIndex = index;
        image.sprite = face;
        model = modelobj;
        weapontype = type;
        uiCharacter = model.gameObject.GetComponent<UIModeCharacter>();
        player = _player;
    }

    public void ButtonClicked()
    {
        characterUI.SlotClick(characterIndex);
    }
}
