using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    Canvas canvas;

    public Stamina stamina;
    public LevelHP levelHp;
    public PartyUI partyUI;
    public SkillUI skillUI;
    public MiniMap map;

    int selnum = 0;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        levelHp.LevelHPView(selnum);
    }

    public void CanvasActive(bool Active)
    {
        canvas.enabled = Active;
    }

    public void LevelHPView(int SelectNumber)
    {
        levelHp.LevelHPView(SelectNumber);
        selnum = SelectNumber;
    }

    public void PartyChange(int SelectNumber, int Number)
    {
        string Selectstr = "party" + SelectNumber;
        string Numberstr = "party" + Number;

        partyUI.PartyUIChange(Selectstr, Numberstr);
    }

    public void SkillChange(int Number)
    {
        skillUI.SkillIconChange(Number);
    }

    public void MiniMapChange(int Number)
    {
        map.MiniMapChange(Number);
    
    }

    
}
