using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;

    Dictionary<int, string> TalkDatas;

    public GameObject SelectButton;
    public GameObject QuestList;

    public int talkIndex = 1;

    private void OnDisable()
    {
        SelectButton.SetActive(false);
        QuestList.SetActive(false);
    }

    public void talkSetting(Dictionary<int, string> data, string name)
    {
        talkIndex = 1;
        TalkDatas = data;

        nameText.text = name;
        StartCoroutine(TextStart(TalkDatas[talkIndex]));
    }

    public void NextText()
    {
        if (TalkDatas.Count > talkIndex)
        {
            talkIndex++;
            StartCoroutine(TextStart(TalkDatas[talkIndex]));
        }
        else if(TalkDatas.Count == talkIndex)
        {
            SelectButton.SetActive(true);
        }
    }

    IEnumerator TextStart(string text)
    {
        talkText.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in text)
        {
            talkText.text += letter; // �� ���ھ� �ؽ�Ʈ �߰�
            yield return new WaitForSeconds(0.1f); // 0.1�ʸ��� ���
        }
    }
}
