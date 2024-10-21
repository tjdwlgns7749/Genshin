using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Unity.Burst.Intrinsics.X86.Avx;
using Unity.VisualScripting;

public class PartyManager : MonoBehaviour
{
    static PartyManager instance = null;

    public Player[] PartyArry;

    int SelectCharacter;

    public Stamina stamina;


    public static PartyManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindAnyObjectByType<PartyManager>();

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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        StartCoroutine(OffFieldSetting());
    }

    IEnumerator OffFieldSetting()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        SelectCharacter = 0;

        Vector3 tmpPos;
        Quaternion tmpQuater;

        Transform tmp = PartyArry[SelectCharacter].transform;

        for (int i = 0; i < PartyArry.Length; i++)
        {
            if (i != SelectCharacter)
            {
                tmpPos = tmp.position;
                tmpQuater = tmp.rotation;
                PartyArry[i].transform.position = tmpPos;
                PartyArry[i].transform.rotation = tmpQuater;
                PartyArry[i].gameObject.SetActive(false);
            }
        }
    }

    public void Select(int number)
    {
        if (SelectCharacter != number - 1)//현재캐릭터가 아니라면
        {
            ChangeSetting(number - 1);
        }
    }

    void ChangeSetting(int number)
    {
        //SelectCharater = 기존캐릭
        //number = 바꿀캐릭
        Vector3 tmpPos;
        Quaternion tmpQuater;

        Transform tmp = PartyArry[SelectCharacter].transform;

        //위치 방향
        tmpPos = tmp.position;
        tmpQuater = tmp.rotation;
        PartyArry[number].transform.position = tmpPos;
        PartyArry[number].transform.rotation = tmpQuater;

        //캐릭터
        PartyArry[SelectCharacter].gameObject.SetActive(false);
        PartyArry[number].gameObject.SetActive(true);

        //카메라
        CameraManager.Instance.TargetChange(PartyArry[number].cameraTarget);

        //UI
        stamina.Target = PartyArry[number].staminaTarget;
        UIManager.Instance.CharacterChange(SelectCharacter, number);

        SelectCharacter = number;
        PlayerManager.Instance.setSelectCharacter(PartyArry[SelectCharacter]);
    }
}
