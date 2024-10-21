using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Image elementalSkill;
    public Image elementalburstSkill;

    PartyManager partyMgr;

    private void Start()
    {
        partyMgr = PartyManager.Instance;
        SkillIconChange(0);
    }

    public void SkillIconChange(int Number)
    {
        elementalSkill.sprite = partyMgr.PartyArry[Number].elementalSkillIcon;
        elementalburstSkill.sprite = partyMgr.PartyArry[Number].elementalBurstIcon;
    }
}
