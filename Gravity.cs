using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // �߷� ���ӵ�
    public float gravity = -9.81f;

    // ĳ������ ���� �ӵ�
    private float verticalVelocity = 0;

    void Update()
    {
        // �߷¿� ���� ���� �ӵ� ����
        verticalVelocity += gravity * Time.deltaTime;

        // ĳ������ �̵�
        Vector3 moveDirection = new Vector3(0, verticalVelocity, 0);
        transform.position += moveDirection * Time.deltaTime;
    }
}
