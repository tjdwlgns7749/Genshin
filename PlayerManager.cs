using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager instance = null;

    public Player[] myCharacterArry;
    public Player SelectCharacter;

    [SerializeField] string UserName = "DefaultName";
    [SerializeField] int UserLevel = 0;
    [SerializeField] float runValue = 0.5f;

    public int Gold { get; set; } = 0;
    float m_Stamina = 100;
    bool m_run = false;
    bool AllDie = false;

    bool b_FightMode = false;
    float FightModeTime = 0.0f;

    string LevelDataCsvFileName = "LevelData.csv";
    Dictionary<int, int> LevelDatas;

    public static PlayerManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<PlayerManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
        Init();
    }

    private void Start()
    {
        List<Dictionary<string, string>> LevelCsv = CSV.Read(LevelDataCsvFileName);

        LevelDatas = new Dictionary<int, int>();

        foreach(var row in  LevelCsv)
        {
            int level = int.Parse(row["Level"]);
            int Max = int.Parse(row["MAX"]);

            LevelDatas[level] = Max;
        }
    }

    void Init()
    {
        SelectCharacter = PartyManager.Instance.PartyArry[0];
    }

    public void setSelectCharacter(Player player)
    {
        SelectCharacter = player;
    }

    public Player getSelectCharacter()
    {
        return SelectCharacter;
    }

    public float Stamina
    {
        get { return m_Stamina; }
        set { m_Stamina = Mathf.Clamp(value, 0f, 100f); }
    }

    private void Update()
    {
        Run();

        if (!AllDie)
        {
            if (DeadCheck())
            {
                AllDead();
            }
        }
    }

    void Run()
    {
        if (m_run)
        {
            Stamina -= runValue;
        }
        else
        {
            Stamina += runValue;
        }
    }

    public void StaminaUse(bool run)
    {
        m_run = run;
    }

    public (int, Sprite, GameObject, WeaponType, Player)[] CharacterSlotData()
    {
        List<(int, Sprite, GameObject, WeaponType, Player)> slotDatas = new List<(int, Sprite, GameObject, WeaponType, Player)>();

        for (int i = 0; i < myCharacterArry.Length; i++)
        {
            slotDatas.Add((i, myCharacterArry[i].faceSprite, myCharacterArry[i].UIModeObject, myCharacterArry[i].eWeaponType, myCharacterArry[i]));
        }

        return slotDatas.ToArray();
    }

    public (string,int,float,float) InfoGetData(int index)
    {
        return (myCharacterArry[index].Name, myCharacterArry[index].Level, myCharacterArry[index].MAXHP, myCharacterArry[index].EquitWeapon.optionvalue);
    }

    bool DeadCheck()
    {
        for(int i =0;i<myCharacterArry.Length;i++)
        {
            if (!myCharacterArry[i].isDead)
            {
                return false;
            }
        }
        return true;
    }

    void AllDead()
    {
        AllDie = true;
        GameManager.Instance.GameOverUI(true);
    }

    public void RespawnSetting()
    {
        for(int i=0;i<myCharacterArry.Length;i++)
        {
            myCharacterArry[i].transform.position = this.transform.position;
            myCharacterArry[i].ReSpawnSetting();
        }
        PartyManager.Instance.Select(1);
        AllDie = false;
    }

    public int GetMaxEXP(int level)
    {
        return LevelDatas[level];
    }

    public void FightMode()
    {
        if (b_FightMode)
            FightModeTime = 0.0f;
        else
        {
            AudioManager.Instance.PlayBGM("MonsterOST");
            b_FightMode = true;
            StartCoroutine(FightTimeCheck());
        }
    }

    IEnumerator FightTimeCheck()
    {
        while(FightModeTime < 10.0f)
        {
            yield return new WaitForSeconds(1.0f);
            FightModeTime += 1.0f;
        }
        AudioManager.Instance.PlayBGM("TownOST");
        b_FightMode = false;
        FightModeTime = 0.0f;
    }
}

public class CSV
{
    /// <summary>
    /// ��ȭ ���� ǥ���ϴ� "����ǥ �ȿ�, ������ ���� �� ������, �� ������ ����"�ϰ� ��ǥ ������ ������.
    /// </summary>
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    /// <summary>
    /// �� �ٲ��� ���Խ� \r or \n or \r\n or \n\r
    /// </summary>
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    /// <summary>
    /// trim�� ���� ���ſ� ��� �Ǵµ� ������ ���� ���� ��� �ش� ������ �����Ѵ�.
    /// </summary>
    static char[] TRIM_CHARS = { ' ' };

    /// <summary>
    /// �ݵ�� file�� Resources ������ ��ġ�ؾ߸� �Ѵ�.
    /// </summary>
    public static List<Dictionary<string, string>> Read(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new System.ArgumentNullException("name");

        // name���� Ȯ���ڸ� ����.
        name = System.IO.Path.GetFileNameWithoutExtension(name);
        TextAsset data = Resources.Load<TextAsset>(name);
        if (!data) throw new System.Exception(string.Format("Not Find TextAsset : {0}", name));

        // ���ڸ� ����� ���� �ƴ϶�� boxing, unboxing�� �߻��ϴ� object ��� string���� �״�� �̿��ϴ� ���� ����.
        var list = new List<Dictionary<string, string>>();
        // �� ������ text ������ ������.
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        // ù ���� �з��̱� ������ 2�̻� �̾�� �Ѵ�.
        if (lines.Length <= 1) return list;

        // �з��� ��ǥ(,) ������ ������.
        var header = Regex.Split(lines[0], SPLIT_RE);
        // �������� ���� index�� 1.
        for (var i = 1; i < lines.Length; i++)
        {
            // �����͸� ��ǥ(,) ������ ������.
            var values = Regex.Split(lines[i], SPLIT_RE);
            // �ش� ���ο� �����Ͱ� �ϳ��� ���ٸ� �ǳʶڴ�.
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            // �з����� �����Ͱ� ���ų� ���� ��츦 ����Ͽ� �ּ����� �������� ����.
            var count = Mathf.Min(header.Length, values.Length);
            for (var j = 0; j < count; j++)
            {
                string value = values[j];
                // ���ڿ��� ���۰� ���� �ִ� TRIM_CHARS�� ����.
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS);//.Replace("\\", "");
                /*object finalValue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) finalValue = n;
                else if (float.TryParse(value, out f)) finalValue = f;*/
                value = value.Replace("<br>", "\n"); // ���๮�ڰ� <br>�� ��� \n�� ����.
                value = value.Replace("<c>", ","); // ��ǥ�� <c>�� ��� ","�� ����.
                entry[header[j]] = value; // �з��� ������ ������ �߰�.
            }

            //var arr = entry["Drop"].Split(';');


            // �з� ���� ������ ������ Dictionary�� List�� �߰�.
            list.Add(entry);
        }
        return list;
    }
}