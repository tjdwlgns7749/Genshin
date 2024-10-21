using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // 중력 가속도
    public float gravity = -9.81f;

    // 캐릭터의 수직 속도
    private float verticalVelocity = 0;

    void Update()
    {
        // 중력에 따라 수직 속도 갱신
        verticalVelocity += gravity * Time.deltaTime;

        // 캐릭터의 이동
        Vector3 moveDirection = new Vector3(0, verticalVelocity, 0);
        transform.position += moveDirection * Time.deltaTime;
    }
}
