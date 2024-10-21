using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHili : Monster
{
    public Transform ArrowStartPos;
    public UseArrow useArrow;

    private new void Start()
    {
        base.Start();
    }

    void getArrow()
    {
        useArrow.GetArrow(ArrowStartPos.position, Damage, skillDatas[0], transform.forward, useArrow);
    }
}
