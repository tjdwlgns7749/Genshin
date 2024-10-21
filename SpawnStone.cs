using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStone : MonoBehaviour, Interaction
{
    public Transform SpawnPos;
    BoxCollider boxcol;
    [SerializeField] public InteractionType Type { get; set; } = InteractionType.SpawnObj;

    private void Start()
    {
        boxcol = GetComponent<BoxCollider>();
    }

    public void ClickInteraction()
    {
        Monster monster = MonsterManager.Instance.MonsterCreat(SpawnPos, 2);
        GameManager.Instance.BossHPUI(true);
        AudioManager.Instance.PlayBGM("BossOST");
    }

    public void SelectButton()
    {

    }
}
