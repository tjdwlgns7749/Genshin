using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using static UnityEditor.Progress;
#endif


public class ItemManager : MonoBehaviour
{
    static ItemManager instance = null;

    string ItemDataCsvFileName = "ItemData.csv";
    string OptionCsvFileName = "OptionData.csv";
    string DropTableFileName = "DropTable.csv";
    string WeaponLevelFileName = "WeaponLevelData.csv";
    Dictionary<int, ItemData> ItemDatas;
    Dictionary<int, ItemOption> OptionDatas;
    Dictionary<int, DropData> DropDatas;
    Dictionary<int, int> WeaponLevelDatas;

    

    public static ItemManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<ItemManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(this!=Instance)
            Destroy(gameObject);
    }

    private void Start()
    {
        List<Dictionary<string, string>> ItemCsv = CSV.Read(ItemDataCsvFileName);
        List<Dictionary<string, string>> OptionCsv = CSV.Read(OptionCsvFileName);
        List<Dictionary<string, string>> DropTableCsv = CSV.Read(DropTableFileName);
        List<Dictionary<string,string>> WeaponLevelCsv = CSV.Read(WeaponLevelFileName);


        ItemDatas = new Dictionary<int, ItemData>();
        OptionDatas = new Dictionary<int, ItemOption>();
        DropDatas = new Dictionary<int, DropData>();
        WeaponLevelDatas = new Dictionary<int, int>();

        foreach (var row in ItemCsv)//전체아이템 데이터
        {
            int id = int.Parse(row["ID"]);
            string name = row["Name"];
            itemType type = System.Enum.Parse<itemType>(row["Type"]);
            int option = int.Parse(row["Option"]);
            string spriteName = row["Image"];
            Sprite sprite = GetImage(spriteName);
            string iconName = row["Icon"];
            Sprite icon = GetImage(iconName);
            int maxCount = int.Parse(row["MaxCount"]);
            string korName = row["Kor"];
            ItemDatas[id] = new ItemData(id, name, type, option, sprite, icon, maxCount, korName);
        }

        foreach (var row in OptionCsv)//아이템옵션 데이터
        {
            int id = int.Parse(row["ID"]);
            itemOption Option = System.Enum.Parse<itemOption>(row["Option"]);
            int Value = int.Parse(row["Value"]);
            string Etc = row["Etc"];
            WeaponType Type = System.Enum.Parse<WeaponType>(row["WeaponType"]);
            OptionDatas[id] = new ItemOption(id, Option, Value, Etc, Type);
        }

        foreach (var row in DropTableCsv)//드랍테이블 데이터
        {
            // ID 가져오기
            int id = int.Parse(row["ID"]);

            // Drop 값 가져오기
            string dropData = row["Drop"];
            string[] s_dropItems = dropData.Split(';');
            int[] dropItems = new int[s_dropItems.Length];

            for (int i = 0; i < s_dropItems.Length; i++)
            {
                dropItems[i] = int.Parse(s_dropItems[i]);
            }
            
            string perData = row["Per"];
            string[] s_perValues = perData.Split(';');
            int[] perValues = new int[s_perValues.Length];

            for(int i=0;i<s_perValues.Length;i++) 
            {
                perValues[i] = int.Parse(s_perValues[i]);
            }

            DropDatas[id] = new DropData(dropItems, perValues);
        }

        foreach (var row in WeaponLevelCsv)
        {
            int level = int.Parse(row["Level"]);
            int Max = int.Parse(row["MAX"]);

            WeaponLevelDatas[level] = Max;
        }

        StartItem();
    }

    void StartItem()
    {
        CreatItem(1);
        CreatItem(3);

        CreatItems(5, 10);
        CreatItems(6, 10);
        CreatItems(7, 10);
        CreatItems(8, 10);

    }

    public void CreatItems(int key, int count)
    {
        for(int i =0;i< count; i++)
        {
            CreatItem(key);
        }
    }

    public void CreatItem(int key)
    {
        Item item = null;

        switch (ItemDatas[key].Type)
        {
            case itemType.Weapon:
                {
                    item = new Weapon();
                    break;
                }
            case itemType.Consumable:
                {
                    item = new Consumable();
                    break;
                }
            case itemType.Etc:
                {
                    item = new Etc();
                    break;
                }
            case itemType.Gold:
                {
                    PlayerManager.Instance.Gold += OptionDatas[ItemDatas[key].Option].Value;
                    return;
                }
        }
        item.ItemDataSetting(ItemDatas[key], OptionDatas[ItemDatas[key].Option], WeaponLevelDatas);
        InventoryManager.Instance.AddItem(item);
    }

    public void CreatDropItem(int key,Vector3 pos)
    {
        for(int i = 0; i < DropDatas[key].Per.Length; i++)
        {
            if (DropCheck(DropDatas[key].Per[i]))
            {
                GameObject cubePrefab = Resources.Load<GameObject>("DropItem");

                GameObject cubeInstance = Instantiate(cubePrefab, pos, Quaternion.identity);

                DropItem dropItemComponent = cubeInstance.GetComponent<DropItem>();

                if(dropItemComponent != null ) 
                {
                    dropItemComponent.DropSetting(DropDatas[key].Drop[i]);
                }
            }
        }
    }

    bool DropCheck(int per)
    {
        int randomNum = UnityEngine.Random.Range(0, 100);

        if(randomNum < per)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public (int , string) DropItemGetKeyIcon(int key)
    {
        ItemData itemData = ItemDatas[key];
        return (itemData.ID, itemData.Name);
    }

    public Sprite getItemIcon(int key)
    {
        return ItemDatas[key].Icon;
    }

    Sprite GetImage(string fileName)
    {
        string folderPath = "ItemImage/";
        string path = folderPath + fileName;

        return Resources.Load<Sprite>(path);
    }

    public int GetMaxEXP(int level)
    {
        return WeaponLevelDatas[level];
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
}
