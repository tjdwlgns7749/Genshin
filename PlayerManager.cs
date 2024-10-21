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
    /// 대화 등을 표기하는 "따옴표 안에, 쉼포가 있을 수 있으니, 그 조건을 제외"하고 쉼표 단위로 나눈다.
    /// </summary>
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    /// <summary>
    /// 줄 바꿈의 정규식 \r or \n or \r\n or \n\r
    /// </summary>
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    /// <summary>
    /// trim은 공백 제거에 사용 되는데 조건을 쓰면 공백 대신 해당 조건을 제거한다.
    /// </summary>
    static char[] TRIM_CHARS = { ' ' };

    /// <summary>
    /// 반드시 file은 Resources 폴더에 위치해야만 한다.
    /// </summary>
    public static List<Dictionary<string, string>> Read(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new System.ArgumentNullException("name");

        // name에서 확장자를 제외.
        name = System.IO.Path.GetFileNameWithoutExtension(name);
        TextAsset data = Resources.Load<TextAsset>(name);
        if (!data) throw new System.Exception(string.Format("Not Find TextAsset : {0}", name));

        // 숫자를 사용할 것이 아니라면 boxing, unboxing이 발생하는 object 대신 string으로 그대로 이용하는 것이 좋다.
        var list = new List<Dictionary<string, string>>();
        // 줄 단위로 text 파일을 나눈다.
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        // 첫 줄은 분류이기 때문에 2이상 이어야 한다.
        if (lines.Length <= 1) return list;

        // 분류를 쉼표(,) 단위로 나눈다.
        var header = Regex.Split(lines[0], SPLIT_RE);
        // 데이터의 시작 index는 1.
        for (var i = 1; i < lines.Length; i++)
        {
            // 데이터를 쉼표(,) 단위로 나눈다.
            var values = Regex.Split(lines[i], SPLIT_RE);
            // 해당 라인에 데이터가 하나도 없다면 건너뛴다.
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            // 분류보다 데이터가 적거나 많을 경우를 대비하여 최소한의 루프만을 실행.
            var count = Mathf.Min(header.Length, values.Length);
            for (var j = 0; j < count; j++)
            {
                string value = values[j];
                // 문자열의 시작과 끝에 있는 TRIM_CHARS를 제거.
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS);//.Replace("\\", "");
                /*object finalValue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) finalValue = n;
                else if (float.TryParse(value, out f)) finalValue = f;*/
                value = value.Replace("<br>", "\n"); // 개행문자가 <br>일 경우 \n로 변경.
                value = value.Replace("<c>", ","); // 쉼표가 <c>일 경우 ","로 변경.
                entry[header[j]] = value; // 분류에 정리한 데이터 추가.
            }

            //var arr = entry["Drop"].Split(';');


            // 분류 별로 정리된 데이터 Dictionary를 List에 추가.
            list.Add(entry);
        }
        return list;
    }
}