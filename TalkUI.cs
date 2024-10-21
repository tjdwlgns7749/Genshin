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
        talkText.text = ""; // 텍스트 초기화

        foreach (char letter in text)
        {
            talkText.text += letter; // 한 글자씩 텍스트 추가
            yield return new WaitForSeconds(0.1f); // 0.1초마다 대기
        }
    }
}
