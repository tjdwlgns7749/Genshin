using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    static MonsterManager instance = null;

    public GameObject[] MonsterArry;

    public static MonsterManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindAnyObjectByType<MonsterManager>();
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

    public Monster MonsterCreat(Transform spawnPos,int MonsterNumber)
    {
        GameObject monsterPrefab = MonsterArry[MonsterNumber];
        GameObject newMonsterObject = Instantiate(monsterPrefab,spawnPos.position,spawnPos.rotation);
        Monster newMonsterComponent = newMonsterObject.GetComponent<Monster>();

        return newMonsterComponent;
    }
}
