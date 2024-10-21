using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDahaka : Monster
{
    public ParticleSystem smallQuakeEffect;
    public ParticleSystem bigQuakeEffect;
    public ParticleSystem breathEffect;

    public Breath breath;
    public EarthQuake earthQuake;
    public Dash dash;

    private void Update()
    {
        base.Update();
        UIManager.Instance.bossHPUI.Setting(KorName, HP, MAXHP, elemental_State);
    }

    new void Start()
    {
        base.Start();
    }

    void useDash()
    {
        dash.usedash(Damage, skillDatas[0]);
    }

    void useEarthquake()
    {
        earthQuake.useEarthQuake(Damage, skillDatas[1]);
    }

    void useBreath()
    {
        breath.useBreath(Damage, skillDatas[2]);
    }

    public void effecttest()
    {
        smallQuakeEffect.Play();
    }

    public void effecttest2()
    {
        bigQuakeEffect.Play();
    }

    public void brathtest()
    {
        breathEffect.Play();
    }

    public void brathtest2()
    {
        breathEffect.Stop();
    }

    public void breathcollider(int check)
    {
        if (check == 0)
            breath.breathCollider(false);
        else
            breath.breathCollider(true);
    }

    public void BossDieBGMReset()
    {
        AudioManager.Instance.PlayBGM("TownOST");
    }
}
