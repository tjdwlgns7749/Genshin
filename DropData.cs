using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropData
{
    public int[] Drop { get; set; }
    public int[] Per { get; set; }

    public DropData(int[] drop, int[] per)
    {
        Drop = drop;
        Per = per;
    }
}
