using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("InGameUI")]
    public InGameUI inGameUI;

    [Header("MenuUI")]
    public MenuUI menuUI;

    [Header("InventoryUI")]
    public InventoryUI inventoryUI;

    [Header("CharacterUI")]
    public CharacterUI characterUI;

    [Header("TalkUI")]
    public TalkUI talkUI;

    [Header("InteractionUI")]
    public InteractionUI interationUI;

    [Header("QuestUI")]
    public QuestUI QuestUI;

    [Header("QuestList")]
    public QuestList questList;

    [Header("SelectButton")]
    public GameObject selectButton;

    [Header("AcceptQuestUI")]
    public  AcceptQuestUI acceptQuestUI;

    [Header("GameOverUI")]
    public GameOverUI gameOverUI;

    [Header("BossHPUI")]
    public BossHPUI bossHPUI;

    [Header("MainMenu")]
    public MainMenu mainmenu;

    [Header("Loading")]
    public Loading loading;

    static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIManager>();

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

    public void CharacterChange(int SelectNumber, int Number)
    {
        //SelectNumber = 기존 캐릭터 
        //Number = 변경할 캐릭터     
        inGameUI.LevelHPView(Number);
        inGameUI.PartyChange(SelectNumber + 1, Number + 1);
        inGameUI.SkillChange(Number);
        inGameUI.MiniMapChange(Number);
    }

    public void GameCanvasUI(bool Active)
    {
        inGameUI.CanvasActive(Active);
    }

    public void MainMenu(bool Active)
    {
        menuUI.gameObject.SetActive(Active);
    }

    public void Inventory(bool Active)
    {
        inventoryUI.gameObject.SetActive(Active);
        menuUI.gameObject.SetActive(!Active);
    }

    public void Character(bool Active)
    {
        characterUI.gameObject.SetActive(Active);
        menuUI.gameObject.SetActive(!Active);
    }

    public void Interaction(bool Active)
    {
        interationUI.gameObject.SetActive(Active);
    }

    public void InteractionTarget(Interaction target)
    {
        interationUI.InteractionTarget(target);
    }

    public void Talk(bool Active)
    {
        talkUI.gameObject.SetActive(Active);

    }

    public void TalkSetData(Dictionary<int, string> data, string name)
    {
        talkUI.talkSetting(data, name);
    }

    public void Quest(bool Active)
    {
        QuestUI.gameObject.SetActive(Active);
    }

    public void QuestSetData(int id)
    {
        QuestUI.SetData(id);
    }

    public void QuestList(bool Active)
    {
        questList.gameObject.SetActive (Active);
    }

    public void QuestListSetData(int[] ints)
    {
        questList.SetData(ints);
    }

    public void SelectButton(bool Active)
    {
        selectButton.SetActive(Active);
    }

    public void AcceptQuest(bool Active)
    {
        acceptQuestUI.gameObject.SetActive(Active);
    }

    public void GameOver(bool Active)
    {
        gameOverUI.gameObject.SetActive(Active);
    }

    public void BossHP(bool Active)
    {
        bossHPUI.gameObject.SetActive(Active);
    }

    public void Mainmenu(bool Active)
    {
        mainmenu.gameObject.SetActive(Active);
    }

    public void Loading(bool Active)
    {
        loading.gameObject.SetActive(Active);
    }
}
