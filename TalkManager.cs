using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    static TalkManager instance = null;

    string TalkDataFileName = "PaimonTalkData.csv";
    Dictionary<int, string> PaimonTalkDatas;

    public Paimon npc_paimon;

    public static TalkManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<TalkManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(this!=Instance)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        List<Dictionary<string, string>> TalkCsv = CSV.Read(TalkDataFileName);

        PaimonTalkDatas = new Dictionary<int, string>();

        foreach(var row in TalkCsv)
        {
            int id = int.Parse(row["ID"]);
            string text = row["Text"];

            PaimonTalkDatas[id] = text;
        }

        npc_paimon.TalkDataSettin(PaimonTalkDatas);
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
