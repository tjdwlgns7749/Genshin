using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum Monster_State
{
    Idle,
    Fight
}

public enum Monster_Pattern
{
    Move,
    Attack,
    Stand
}

public enum Elemental_State
{
    Normal,
    Fire,
    Wind,
    Ice
}


public class Monster : MonoBehaviour, Damageable
{

    public float HP = 0;
    public float MAXHP = 0;
    public int Level = 0;
    public string Name = "abcd";
    public float Damage = 0;
    public Elemental_State elemental_State;
    public Monster_State monster_State;
    public Monster_Pattern monster_Pattern;
    public int DropTableKey;
    public GameObject HpBar;
    public string KorName;

    public Billboard billboard;

    public bool isDead { get; set; } = false;

    MonsterData data;
    public MonsterData monsterData;

    bool[] AttackCoolTime;
    public SkillData[] skillDatas;

    MonsterAnimation monsterAnimation;
    ElementalSystem elementalSystem;
    MonsterAI monsterAI;
    public MonsterAttack Attack;

    protected void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        elementalSystem = GetComponent<ElementalSystem>();
        monsterAI = GetComponent<MonsterAI>();
        Init();
    }

    void Init()
    {
        data = monsterData;//몬스터 초기 데이터 받아오기
        StatusSetting();//몬스터의 스테이터스 초기세팅
    }

    void StatusSetting()
    {
        Name = data.MonsterName;
        HP = data.MAXHP;
        MAXHP = data.MAXHP;
        Level = data.Level;
        Damage = data.Damage;
        AttackCoolTime = new bool[skillDatas.Length];

        for(int i=0;i<skillDatas.Length;i++)
        {
            AttackCoolTime[i] = skillDatas[i].active;
        }
    }
    protected void Update()
    {
        monsterAI.Updates();
        billboard.Elemental_State(elemental_State);
    }

    public void GetDamage(float Damage,int num = 0)//스킬의 데미지와 속성
    {
        if(!isDead)
        {
            if (gameObject.tag != "Boss")
            PlayerManager.Instance.FightMode();

            HP -= Damage;

            billboard.DamageGet(Damage, num);

            float HpPercent = HP / MAXHP;

            if (HpPercent < 0)
                HpPercent = 0.0f;
            if (HpBar != null)
                HpBar.transform.localScale = new Vector3(HpPercent, 1, 1);
            
            if (HP <= 0)
            {
                HP = 0;

                isDead = true;

                Die();
            }
        }
    }

    public void SkillElementalCheck(float Damage, int ElementalNumber, SortedSet<int> ints)//스킬의 속성과 데미지
    {
        GetDamage(elementalSystem.ElementalCheck(Damage, ElementalNumber, (int)elemental_State, ints), ElementalNumber);
    }

    public void Getorder(Monster_Pattern pattern)
    {
        switch (pattern)
        {
            case Monster_Pattern.Move:
                MoveAnimation(true);
                break;
            case Monster_Pattern.Attack:
                MoveAnimation(false);
                int select = Attack.Attack(AttackCoolTime);
                string str = "Attack" + select;
                StartCoroutine(CoolTime(select - 1));
                PlayTriggerAnimation(str);
                break;
            case Monster_Pattern.Stand:
                MoveAnimation(false);
                break;
        }
    }

    public bool CoolTimeCheck()
    {
        for (int i = 0; i < AttackCoolTime.Length; i++)
        {
            if (AttackCoolTime[i])
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator CoolTime(int number)
    {
        AttackCoolTime[number] = false;
        yield return new WaitForSecondsRealtime(skillDatas[number].coolTime);

        AttackCoolTime[number] = true;
    }

    void Die()
    {
        QuestManager.Instance.ClearCheck(Name);
        monsterAnimation.PlayTrigger("Die");
        ItemManager.Instance.CreatDropItem(DropTableKey, this.transform.position);
        GameManager.Instance.BossHPUI(false);
    }

    void Destroy()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void MoveAnimation(bool Move)
    {
        monsterAnimation.MoveAnimation(Move);
    }

    public void getVelocity(float Speed)
    {
        monsterAnimation.Velocity(Speed);
    }

    public void PlayTriggerAnimation(string AniName)
    {
        monsterAnimation.PlayTrigger(AniName);
    }

}
