using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;
    public Sprite[] elemental_Sprites;
    public SpriteRenderer sprite;
    public Transform pos;
    public int ReadyCount = 0;
    public DamageView ViewPrefab;

    Queue<DamageView> DamageViewPool = new Queue<DamageView>();

    private void Start()
    {
        cam = Camera.main.transform;
        GameSetting(ReadyCount);
    }

    void GameSetting(int count)
    {
        for(int i = 0; i < count; i++) 
        {
            DamageViewPool.Enqueue(CreateView(ViewPrefab));
        
        }
    }

    DamageView CreateView(DamageView damageView)
    {
        var newView = Instantiate(damageView);
        newView.gameObject.SetActive(false);
        newView.transform.SetParent(transform.Find("Canvas"));
        newView.SetPos(pos);
        newView.SetBillboard(this);

        return newView;
    }

    public void Elemental_State(Elemental_State state)
    {
        if(state == global::Elemental_State.Fire)
        {
            sprite.sprite = elemental_Sprites[0];
        }
        else if(state == global::Elemental_State.Ice)
        {
            sprite.sprite = elemental_Sprites[1];
        }
        else
        {
            sprite.sprite = null;
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward * -1, cam.rotation * Vector3.up);
    }

    public void DamageGet(float Damage , int Elemental)
    {
        var newView = DamageViewPool.Dequeue();
        newView.gameObject.SetActive(true);
        newView.GetDamage(Damage , Elemental);
    }

    public void ReturnObject(DamageView view)
    {
        view.gameObject.SetActive(false);
        view.SetPos(pos);
        DamageViewPool.Enqueue(view);
    }
}
