using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundCheck : MonoBehaviour
{
    [Header("Check Property")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distance = 0.0f;
    [SerializeField] private float radius = 0.2f;

    int Checklayer;

    [Header("Debug")]
    [SerializeField] private bool drawGizmo;

    private void Start()
    {
        Checklayer = LayerMask.NameToLayer("Ground");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (drawGizmo)
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - distance, transform.position.z), radius);
    }

    public bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - distance, transform.position.z), radius);
        for(int i =0;i < colliders.Length;i++)
        {
            if (colliders[i].gameObject.layer == Checklayer)
                return true;
        }
        return false;   
    }
}
