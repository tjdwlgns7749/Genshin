using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine;
public sealed class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    public PlayerManager user;
    public PartyManager partyMgr;
    public UIManager uiMgr;
    public MonsterManager monsterMgr;
    public ItemManager itemMgr;
    public InventoryManager invenMgr;
    public CameraManager camMgr;
    public TalkManager talkMgr;
    public QuestManager questMgr;
    public AudioManager audioMgr;

    bool cursor;
    public bool GameMode; //UI필요할땐 false Game으로 들어가면 true

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
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

    private void Start()
    {
        partyMgr = PartyManager.Instance;
        uiMgr = UIManager.Instance;
        monsterMgr = MonsterManager.Instance;
        itemMgr = ItemManager.Instance;
        invenMgr = InventoryManager.Instance;
        talkMgr = TalkManager.Instance;
        

        GameModeReady(false);//나중에 메인화면 구현하면 바꿔야함
    }

    void GameModeReady(bool Active)
    {
        if (Active)//게임할때
        {
            //커서끄고 게임모드ON
            cursor = false;
            GameMode = true;
            Time.timeScale = 1;
        }
        else//UI모드
        {
            cursor = true;
            GameMode = false;
            Time.timeScale = 0;
        }
    }

    void TalkMode(bool Active)
    {
        if (Active)//게임할때
        {
            //커서끄고 게임모드ON
            cursor = false;
            GameMode = true;
        }
        else//UI모드
        {
            cursor = true;
            GameMode = false;
        }
    }

    private void Update()
    {
        MouseCursorActive(cursor);


    }

    public void MainMenu(bool Active)
    {
        uiMgr.MainMenu(Active);
        uiMgr.GameCanvasUI(!Active);
        GameModeReady(!Active);
    }

    public void Inventory(bool Active)
    {
        uiMgr.Inventory(Active);
        uiMgr.MainMenu(!Active);
    }

    public void CharacterUI(bool Active)
    {
        uiMgr.Character(Active);
        uiMgr.MainMenu(!Active);
    }

    public void InteractionUI(bool Active)
    {
        uiMgr.Interaction(Active);
    }

    public void InteractionTarget(Interaction target)
    {
        uiMgr.InteractionTarget(target);
    }

    public void TalkUI(bool Active)
    {
        uiMgr.Talk(Active);
        uiMgr.GameCanvasUI(!Active);
        TalkMode(!Active);
        CameraManager.Instance.ControllerOnOff(!Active);
    }

    public void TalkUISetData(Dictionary<int, string> data, string name)
    {
        uiMgr.TalkSetData(data, name);
    }

    public void QuestUI(bool Active)
    {
        uiMgr.Quest(Active);
        uiMgr.Talk(!Active);
        uiMgr.GameCanvasUI(Active);
    }

    public void QuestUISetData(int id)
    {
        uiMgr.QuestSetData(id);
    }

    public void QuestList(bool Active)
    {
        uiMgr.SelectButton(!Active);
        uiMgr.QuestList(Active);
    }

    public void QuestListSetData(int[] ints)
    {
        uiMgr.QuestListSetData(ints);
    }


    public void QuestAcceptButtonClick()
    {
        uiMgr.Quest(false);
        TalkMode(true);
        CameraManager.Instance.ControllerOnOff(true);
    }

    public void QuestRefuseButtonClick()
    {
        uiMgr.Quest(false);
        TalkMode(true);
        CameraManager.Instance.ControllerOnOff(true);
    }

    public void AcceptQuestUI(bool Active)
    {
        uiMgr.AcceptQuest(Active);
        uiMgr.MainMenu(!Active);
    }

    public void GameOverUI(bool Active)
    {
       StartCoroutine(WaitGameMode(Active));
    }

    public void BossHPUI(bool Active)
    {
        uiMgr.BossHP(Active);
    }

    public void GameStart()
    {
        uiMgr.Mainmenu(false);
        uiMgr.Loading(true);
        GameModeReady(true);
    }
    
    IEnumerator WaitGameMode(bool Active)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (Active)
        {
            uiMgr.GameOver(Active);
            GameModeReady(!Active);
        }
        else
        {
            uiMgr.GameOver(Active);
            GameModeReady(!Active);
            PlayerManager.Instance.RespawnSetting();
        }
    }


    void MouseCursorActive(bool Active)
    {
        if (Active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SelectNum(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int number = int.Parse(context.control.name);
            partyMgr.Select(number);
        }
    }

    public void MouseCursor(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<System.Single>();
        if (GameMode)
        {
            if (value > 0)
            {
                cursor = true;
            }
            else
            {
                cursor = false;
            }
        }
    }

}


