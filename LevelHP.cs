using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelHP : MonoBehaviour
{
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI Leveltext;

    public Slider HPslider;

    PartyManager partyMgr;

    private void Start()
    {
        partyMgr = PartyManager.Instance;
        LevelHPView(0);
    }

    public void LevelHPView(int SelectNumber)
    {
        string str = partyMgr.PartyArry[SelectNumber].Level.ToString();
        Leveltext.text = "Lv." + str;

        str = partyMgr.PartyArry[SelectNumber].HP.ToString();
        string str2 = partyMgr.PartyArry[SelectNumber].MAXHP.ToString();
        HPtext.text = str + "/" + str2;

        HPslider.maxValue = partyMgr.PartyArry[SelectNumber].MAXHP;
        HPslider.value = partyMgr.PartyArry[SelectNumber].HP;
    }
}
