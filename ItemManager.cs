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

        foreach (var row in ItemCsv)//��ü������ ������
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

        foreach (var row in OptionCsv)//�����ۿɼ� ������
        {
            int id = int.Parse(row["ID"]);
            itemOption Option = System.Enum.Parse<itemOption>(row["Option"]);
            int Value = int.Parse(row["Value"]);
            string Etc = row["Etc"];
            WeaponType Type = System.Enum.Parse<WeaponType>(row["WeaponType"]);
            OptionDatas[id] = new ItemOption(id, Option, Value, Etc, Type);
        }

        foreach (var row in DropTableCsv)//������̺� ������
        {
            // ID ��������
            int id = int.Parse(row["ID"]);

            // Drop �� ��������
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
}
