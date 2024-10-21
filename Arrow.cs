using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidBody;
    BoxCollider boxCollider;
    public float ArrowForce = 3.0f;
    HitCheck hitCheck;
    UseArrow arrowMgr;
    public string targetLayerName;
    int targetLayer;

    public Vector3 localEulerAnglesVector;
    Vector3 ShotVector;

    SkillData skillData;
    float Damage = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        hitCheck = GetComponent<HitCheck>();
        targetLayer = LayerMask.NameToLayer(targetLayerName);
    }

    public void UseArrow(Vector3 StartArrowPos, float damage, SkillData skilldata,Vector3 shotDirection,UseArrow useMgr)
    {
        arrowMgr = useMgr; 

        transform.parent = null;
        rigidBody.position = StartArrowPos;

        skillData = skilldata;
        Damage = damage + skillData.Damage;

        ShotVector = shotDirection.normalized;

        rigidBody.AddForce(ShotVector * ArrowForce, ForceMode.VelocityChange);
        StartCoroutine(DestoryArrow());
    }

    IEnumerator DestoryArrow()
    {
        yield return new WaitForSeconds(1.0f);
        if (arrowMgr != null)
            arrowMgr.RetrunArrow(this);
        else
            Destroy(gameObject);
    }

    public void ReturnSetting()
    {
        transform.parent = arrowMgr.transform;
        transform.localPosition = new Vector3(0, 0, 0);
        Vector3 combineAngle = arrowMgr.transform.rotation.eulerAngles + localEulerAnglesVector;
        transform.rotation = Quaternion.Euler(combineAngle);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            hitCheck.AttackColliderCheck(Damage, skillData);

            if (arrowMgr != null)
                arrowMgr.RetrunArrow(this);
            else
                Destroy(gameObject);
        }
    }

}
