using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
#endif
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{
    PartyManager partyMgr;

    //party1
    [Header("Party1")]
    public TextMeshProUGUI party1SelectName;
    public TextMeshProUGUI party1NoneSelectName;
    public Image party1SelectFace;
    public Image party1NoneSelectFace;
    public Slider party1HPSlider;
    //party2
    [Header("Party2")]
    public TextMeshProUGUI party2SelectName;
    public TextMeshProUGUI party2NoneSelectName;
    public Image party2SelectFace;
    public Image party2NoneSelectFace;
    public Slider party2HPSlider;

    private void Start()
    {
        partyMgr = PartyManager.Instance;
        Init();
    }

    void Init()
    {
        //이름
        party1SelectName.text = partyMgr.PartyArry[0].Name;
        party1NoneSelectName.text = partyMgr.PartyArry[0].Name;
        party2SelectName.text = partyMgr.PartyArry[1].Name;
        party2NoneSelectName.text = partyMgr.PartyArry[1].Name;
        //사진
        party1SelectFace.sprite = partyMgr.PartyArry[0].faceSprite;
        party1NoneSelectFace.sprite = partyMgr.PartyArry[0].faceSprite;
        party2SelectFace.sprite = partyMgr.PartyArry[1].faceSprite;
        party2NoneSelectFace.sprite = partyMgr.PartyArry[1].faceSprite;
        //체력슬라이더
        party1HPSlider.maxValue = partyMgr.PartyArry[0].MAXHP;
        party1HPSlider.value = partyMgr.PartyArry[0].HP;
        party2HPSlider.maxValue = partyMgr.PartyArry[1].MAXHP;
        party2HPSlider.value = partyMgr.PartyArry[1].HP;
    }

    void HPUpdate()
    {
        party1HPSlider.maxValue = partyMgr.PartyArry[0].MAXHP;
        party1HPSlider.value = partyMgr.PartyArry[0].HP;
        party2HPSlider.maxValue = partyMgr.PartyArry[1].MAXHP;
        party2HPSlider.value = partyMgr.PartyArry[1].HP;
    }

    public void PartyUIChange(string selectstr , string numstr)
    {
        //selectstr = 기존 캐릭터 
        //numstr = 변경할 캐릭터 
        HPUpdate();

        transform.Find(selectstr).Find("Select").gameObject.SetActive(true);
        transform.Find(selectstr).Find("NoneSelect").gameObject.SetActive(false);

        transform.Find(numstr).Find("Select").gameObject.SetActive(false);
        transform.Find(numstr).Find("NoneSelect").gameObject.SetActive(true);
    }
}
