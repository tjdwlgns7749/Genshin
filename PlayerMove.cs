using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    CharacterController characterController;
    public Camera cam;

    [Header("Move")]
    public float moveSpeed = 0;

    [Header("Rotate")]
    public float rotateSpeed = 0;

    [Header("Jump")]
    public float JumpForce = 0;

    [Header("Gravity")]
    public float Gravity = 9.8f;

    Vector3 Y_Force;
    Vector3 moveDirection;
    Vector2 moveKey;




    private void Start()
    {
        characterController = GetComponent<CharacterController>();      
    }

    public void Updates()
    {
        if (moveKey != Vector2.zero)
        {
            Move();
            Rotate();
        }
        SetGravity();//ม฿ทย
    }

    private void Move()
    {
        if (moveKey.y > 0)//W
            moveDirection += cam.transform.forward;
        else if (moveKey.y < 0)//S
            moveDirection += -(cam.transform.forward);
        
        if (moveKey.x > 0)//D
            moveDirection += cam.transform.right;
        else if (moveKey.x < 0)//A
            moveDirection += -(cam.transform.right);

        moveDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.z).normalized;
    }

    private void Rotate()
    {
        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        Y_Force.y = JumpForce;
    }

    public void GetDirection(Vector2 direction)
    {
        moveKey = direction;
    }
    void SetGravity()
    {
        Y_Force.y += -Gravity * Time.deltaTime;
        if (Y_Force.y < -9.8)
            Y_Force.y = -9.8f;

        if (characterController != null)
            characterController.Move(Y_Force * Time.deltaTime);
    }
}