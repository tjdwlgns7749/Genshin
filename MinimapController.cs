using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // �̴ϸ� ī�޶��� �ʱ� ȸ�� ���� ����
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // �� �����Ӹ��� ȸ���� �ʱ� ȸ������ �ǵ���
        transform.rotation = initialRotation;
    }
}
