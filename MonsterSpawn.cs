using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public int MonsterCount;

    public Transform[] SpawnPos;
    public int[] MonsterType;

    public Monster[] monsterArry;

    bool respawnCheck = false;

    void OnEnable()
    {
        MonsterCreat();
    }

    void Update()
    {
        MonsterDieCheck();
    }
    
    void MonsterDieCheck()
    {
        for(int i =0;i < monsterArry.Length;i++) 
        {
            if (monsterArry[i].isDead == false)
            {
                return;
            }
        }
        if (!respawnCheck)
        {
            respawnCheck = true;
            StartCoroutine(MonsterReSpawn());
        }
    }

    IEnumerator MonsterReSpawn()
    {
        yield return new WaitForSecondsRealtime(30.0f);

        for (int i = 0; i < MonsterCount; i++)
        {
            monsterArry[i] = MonsterManager.Instance.MonsterCreat(SpawnPos[i], MonsterType[i]);
        }
        respawnCheck = false;
    }

    void MonsterCreat()
    {
        for (int i = 0; i < MonsterCount; i++)
        {
            monsterArry[i] = MonsterManager.Instance.MonsterCreat(SpawnPos[i], MonsterType[i]);
        }
    }
}
