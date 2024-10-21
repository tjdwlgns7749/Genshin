using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;


public class DamageView : MonoBehaviour
{
    public TextMeshPro DamageText;
    public float Speed = 2.0f;
    public Billboard billboard;

    private void OnEnable()
    {
        StartCoroutine(view());
    }

    public void UpMove()
    {
        transform.position += Vector3.up * Speed * Time.deltaTime;
    }

    IEnumerator view()
    {
        float StartTime = Time.time;

        while(Time.time - StartTime < 0.3f) 
        {
            UpMove();
            yield return null;
        }

        billboard.ReturnObject(this);
    }

    public void SetPos(Transform transform)
    {
        this.transform.position = transform.position;
        // ���� ȸ������ 180�� ȸ�� �߰�
        this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 180, 0));
    }

    public void SetBillboard(Billboard _billboard)
    {
        billboard = _billboard;
    }

    public void GetDamage(float Damage, int Elemental)
    {
        switch (Elemental)
        {
            case 0: // ���Ӽ�
                DamageText.text = $"<color=white>{Damage}</color>";
                break;

            case 1: // ��
                DamageText.text = $"<color=orange>{Damage}</color>";
                break;

            case 2: // �ٶ�
                break;

            case 3: // ����
                DamageText.text = $"<color=#00FFFF>{Damage}</color>";
                break;
        }
    }
}
