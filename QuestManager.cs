using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static QuestManager instance = null;

    string QuestDataFileName = "QuestData.csv";
    Dictionary<int, QuestData> QuestDatas;
    List<QuestData> AcceptQuestList;

    public Paimon paimon;

    private void Start()
    {
        List<Dictionary<string, string>> QuestCsv = CSV.Read(QuestDataFileName);

        QuestDatas = new Dictionary<int, QuestData>();
        AcceptQuestList = new List<QuestData>();

        foreach(var row in QuestCsv)
        {
            int id = int.Parse(row["ID"]);
            string title = row["Title"];
            int value = int.Parse(row["Value"]);
            int maxValue = int.Parse(row["MaxValue"]);
            int Exp = int.Parse(row["EXP"]);

            string QuestItem = row["Item"];
            string[] s_QuestItems = QuestItem.Split(';');
            int[] QuestItems = new int[s_QuestItems.Length];

            for (int i = 0; i < s_QuestItems.Length; i++)
            {
                QuestItems[i] = int.Parse(s_QuestItems[i]);
            }

            string QuestItemValue = row["ItemValue"];
            string[] s_QuestItemValues = QuestItemValue.Split(';');
            int[] QuestItemValues = new int[s_QuestItems.Length];

            for(int i =0;i<s_QuestItemValues.Length;i++)
            {
                QuestItemValues[i] = int.Parse(s_QuestItemValues[i]);
            }

            string Contents = row["Contents"];

            string Target = row["TargetName"];

            QuestDatas[id] = new QuestData(id, title, Contents, value, maxValue, Exp, QuestItems, QuestItemValues, Target);
        }
    }

    public static QuestManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<QuestManager>();
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

    public QuestData GetQuestData(int id)
    {
        return QuestDatas[id];
    }

    public int ReturnCount()
    {
        return AcceptQuestList.Count;
    }

    public List<QuestData> GetAccepQuestData()
    {
        return AcceptQuestList;
    }

    public void AcceptQuest(int id)
    {
        QuestDatas[id].State = Quest_State.Play;
        AcceptQuestList.Add(QuestDatas[id]);
    }

    public Quest_State getQuestState(int id)
    {
        return QuestDatas[id].State;
    }

    public void QuestClear(int id)
    {
        for(int i =0;i<AcceptQuestList.Count;i++)
        {
            if (AcceptQuestList[i].ID == id)
            {
                for (int j = 0; j < AcceptQuestList[i].Item.Length; j++)
                {

                    ItemManager.Instance.CreatItems(AcceptQuestList[i].Item[j], AcceptQuestList[i].ItemValue[j]);
                }
                AcceptQuestList.RemoveAt(i);
                paimon.questIndex++;
                return;
            }
        }
    }

    public void ClearCheck(string name)
    {
        for(int i =0; i< AcceptQuestList.Count; i++)
        {

            if (AcceptQuestList[i].TargetName == name)
            {
                AcceptQuestList[i].Value++;

                if(AcceptQuestList[i].Value >= AcceptQuestList[i].MaxValue)//Ŭ����� �ʰ�
                {
                    AcceptQuestList[i].Value = AcceptQuestList[i].MaxValue;
                    AcceptQuestList[i].State = Quest_State.Clear;
                }
            }
        }
    }

    public string getQuestTitle(int id)
    {
        return QuestDatas[id].Title;
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
