using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // 미니맵 카메라의 초기 회전 값을 저장
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 매 프레임마다 회전을 초기 회전으로 되돌림
        transform.rotation = initialRotation;
    }
}
