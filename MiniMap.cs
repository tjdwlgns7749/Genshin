using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RawImage Image;
    PartyManager partyMgr;

    private void Start()
    {
        partyMgr = PartyManager.Instance;
        MiniMapChange(0);
    }

    public void MiniMapChange(int Number)
    {
        Image.texture = partyMgr.PartyArry[Number].renderTexture;
    }
}
