using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public enum Player_State
{
    Idle,
    Move,
    Jump,
    Falling,
    Attack
}

public class Player : MonoBehaviour, Damageable
{
    protected PlayerMove playerMove;
    protected PlayerAnimation playerAnimation;
    isGroundCheck GroundCheck;
    public PlayerManager user;
    Rigidbody rigidbody;
    BoxCollider testcol;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public Player_State playerState;
    bool JumpCheck = false;
    bool DropCheck = true;

    public GameObject handWeapon;
    public GameObject idleWeapon;
    public float player_Damage { get; set; }
    public Transform cameraTarget;
    public Transform staminaTarget;
    public string Name;
    public Sprite faceSprite;
    public Sprite elementalSkillIcon;
    public Sprite elementalBurstIcon;
    public RenderTexture renderTexture;
    public GameObject UIModeObject;
    public WeaponType eWeaponType;
    public GameObject[] HandWeaponArry;
    public GameObject[] IdleWeaponArry;
    public string FirstWeaponName;
    public Item EquitWeapon;

    public float MAXHP { get; set; }
    public float HP { get; set; }
    public int Level = 1;
    public int EXP { get; set; }
    public int MAXEXP { get; set; }
    public bool isDead { get; set; } = false;

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
        GroundCheck = GetComponent<isGroundCheck>();
        testcol = GetComponent<BoxCollider>();
        InitSetting();
    }

    void InitSetting()
    {
        playerState = Player_State.Idle;
        EquitWeapon = InventoryManager.Instance.FirstSetting(FirstWeaponName);
        player_Damage = EquitWeapon.optionvalue;
        MAXHP = Level * 2000;
        HP = MAXHP;
        MAXEXP = PlayerManager.Instance.GetMaxEXP(Level);
    }

    protected void Update()
    {
        if (Time.timeScale > 0)
        {
            playerAnimation.Updates();
            if (user.Stamina <= 0)
                RunStaminaCheck();
        }

    }

    protected void FixedUpdate()
    {
        playerMove.Updates();
        JumpDropUpdate();//Á¡ÇÁ,³«ÇÏ Update
    }

    void JumpDropUpdate()
    {
        if (playerState != Player_State.Jump && !GroundCheck.IsGrounded())//³«ÇÏ
            DropDown();
        else if (playerState == Player_State.Falling && GroundCheck.IsGrounded())//³«ÇÏ ÈÄ ÂøÁö
            FallGround("DropDown");
        else if (JumpCheck && GroundCheck.IsGrounded())//Á¡ÇÁ ÈÄ ÂøÁö
            FallGround("Jump");
    }

    void DropDown()//³«ÇÏ
    {
        playerState = Player_State.Falling;
        if (DropCheck)
        { 
            playerAnimation.PlayTrigger("DropDown");
            DropCheck = false;
        }
    }

    void FallGround(string state)//Á¡ÇÁ³ª ³«ÇÏ ÈÄ ÂøÁö
    {
        playerAnimation.MoveAnimation(false);
        playerState = Player_State.Idle;
        playerAnimation.PlayTrigger("FalltoGround");
        if (state == "DropDown")
            DropCheck = true;
        else if (state == "Jump")
            JumpCheck = false;
    }

    IEnumerator JumpCheckWait()
    {
        yield return new WaitForSeconds(0.2f);
        JumpCheck = true;
        yield return null;
    }

    public void GetDamage(float Damage,int number = 0)
    {
        if (!isDead)
        {
            //PlayerManager.Instance.FightMode();
            HP -= Damage;

            if (HP <= 0)
            {
                HP = 0;

                isDead = true;

                DieAnimation();
            }
        }
    }

    public void SkillElementalCheck(float Damage,int ElementalNumber, SortedSet<int> ints)
    {
        GetDamage(Damage);
  
    }

    void RunStaminaCheck()
    {
        playerAnimation.SprintAnimation(false);
        user.StaminaUse(false);
    }

    void DieAnimation()
    {
        playerAnimation.PlayTrigger("Die");
    }

    public void DieEvent()
    {
        if (!PlayerManager.Instance.myCharacterArry[1].isDead)
            PartyManager.Instance.Select(2);
    }

    public void DieEvent2()
    {
        if (!PlayerManager.Instance.myCharacterArry[0].isDead)
            PartyManager.Instance.Select(1);
    }

    public bool DieGet()
    {
        return isDead;
    }

    public void EquitWeaponChange(Item weaponItem)
    {

        //¾ÆÀÌÅÛ
        EquitWeapon = weaponItem;
        //°ø°Ý·Â
        player_Damage = EquitWeapon.optionvalue;

        for(int i = 0; i < HandWeaponArry.Length; i++)
        {
            if (HandWeaponArry[i].name == weaponItem.Data.Name)
            {
                handWeapon.gameObject.SetActive(false);

                handWeapon = HandWeaponArry[i];

                handWeapon.gameObject.SetActive(true);
            }
        }

        for(int i =0; i< IdleWeaponArry.Length; i++)
        {
            if (IdleWeaponArry[i].name == weaponItem.Data.Name)
            {
                idleWeapon.gameObject.SetActive(false);

                idleWeapon = IdleWeaponArry[i];

                idleWeapon.gameObject.SetActive(false);
            }
        }
    }

    public void Heal(int value)
    {
        if (HP + value >= MAXHP)
        {
            HP = MAXHP;
        }
        else
        {
            HP += value;
        }
    }

    public void ReSpawnSetting()
    {
        isDead = false;
        HP = MAXHP;
    }

    public void GetEXP(int value)
    {
        if (EXP + value >= MAXEXP)
        {
            int a = (EXP + value) - MAXEXP;
            LevelUP(a);
        }
        else
            EXP += value;
    }

    public void LevelUP(int value)
    {
        Level++;
        EXP = value;
        MAXEXP = PlayerManager.Instance.GetMaxEXP(Level);
        MAXHP = Level * 2000;
        HP = MAXHP;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.GameMode)
        {
            if (Time.timeScale > 0)
            {
                Vector2 direction = context.ReadValue<Vector2>();
                if (context.performed)
                {
                    if (playerState != Player_State.Falling && playerState != Player_State.Jump)
                    {
                        if (direction != Vector2.zero && (playerState == Player_State.Idle || playerState == Player_State.Move) && GroundCheck.IsGrounded())
                        {
                            playerState = Player_State.Move;
                            playerMove.GetDirection(direction);
                            playerAnimation.MoveAnimation(true);
                            handWeapon.gameObject.SetActive(false);
                            idleWeapon.gameObject.SetActive(true);
                        }
                        else
                        {
                            playerState = Player_State.Idle;
                            playerMove.GetDirection(direction);
                            playerAnimation.MoveAnimation(false);
                        }
                    }
                }
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.GameMode)
        {
            if (Time.timeScale > 0)
            {
                float value = context.ReadValue<System.Single>();
                if (value != 0f && GroundCheck.IsGrounded() && playerState != Player_State.Jump && playerState != Player_State.Attack)
                {
                    playerState = Player_State.Jump;
                    playerMove.Jump();
                    playerAnimation.PlayTrigger("isJump");
                    StartCoroutine(JumpCheckWait());
                }
            }
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            float value = context.ReadValue<System.Single>();
            if (value != 0f && user.Stamina > 0)
            {
                playerAnimation.SprintAnimation(true);
                playerState = Player_State.Idle;
                handWeapon.gameObject.SetActive(false);
                idleWeapon.gameObject.SetActive(true);
                user.StaminaUse(true);
            }
            else
            {
                playerAnimation.SprintAnimation(false);
                user.StaminaUse(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interaction")
        {
            Interaction targetNPC = other.gameObject.GetComponent<Interaction>();
            GameManager.Instance.InteractionUI(true);
            GameManager.Instance.InteractionTarget(targetNPC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interaction")
        {
            GameManager.Instance.InteractionUI(false);
        }
    }
}
