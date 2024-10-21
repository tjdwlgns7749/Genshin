using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    NavMeshAgent agent;
    Monster monster;
    Rigidbody Target;

    public float MinDis;
    public float MaxDis;

    public LayerMask targetLayer;
    public float IdleStateSearchRadius;
    public float FightSearchRadius;
    public bool gizmo = true;
    bool RushAttack = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        monster = GetComponent<Monster>();
        Init();
    }

    void Init()
    {

    }

    public void Updates()
    {
        if (Search(IdleStateSearchRadius) && monster.monster_State == Monster_State.Idle) // 비전투상태에서 적을 찾음
        {
            monster.monster_State = Monster_State.Fight; // 전투상태돌입
            StartCoroutine(FightAI()); // 전투AI 
        }
    }

    IEnumerator FightAI()
    {
        while (true)
        {
            if (monster.monster_Pattern != Monster_Pattern.Attack) // 공격중엔 판단 X
            {
                float distanceToTarget = DistanceToTarget();
                if (MinDis < distanceToTarget && MaxDis > distanceToTarget) // 최소거리보다 멀고 최대거리보다 짧을때
                {
                    agent.isStopped = false;
                    agent.SetDestination(Target.position);
                    monster.monster_Pattern = Monster_Pattern.Move;
                    monster.Getorder(Monster_Pattern.Move);
                }
                else if (MinDis > distanceToTarget && monster.CoolTimeCheck()) // 공격범위
                {
                    agent.isStopped = true;

                    // 타겟을 완전히 쳐다볼 때까지 기다림
                    while (!RotateTowardsTarget())
                    {
                        yield return null;
                    }

                    monster.Getorder(Monster_Pattern.Attack);
                    monster.monster_Pattern = Monster_Pattern.Attack;
                }
                else if (MinDis > distanceToTarget && !monster.CoolTimeCheck())
                {
                    agent.isStopped = false;
                    monster.Getorder(Monster_Pattern.Stand);
                }
                else if (MaxDis < distanceToTarget) // 최대인식 범위를 벗어남
                {
                    agent.isStopped = true;
                    monster.MoveAnimation(false);
                    monster.monster_State = Monster_State.Idle;
                    break;
                }
            }
            Search(FightSearchRadius);
            monster.getVelocity(agent.velocity.magnitude);
            yield return null;
        }
    }

    void RushAttackTargetSearch()
    {
        RushAttack = true;
        StartCoroutine(RushAttackMove());
    }

    void RushAttackSearchEND()
    {
        RushAttack = false;
        agent.isStopped = true;
    }

    IEnumerator RushAttackMove()
    {
        while (true)
        {
            if (RushAttack)
            {
                agent.isStopped = false;
                agent.SetDestination(Target.position);
            }
            else
            {
                agent.isStopped = true;
                break;
            }
            yield return null;
        }
    }

    public void AttackEnd()
    {
        monster.monster_Pattern = Monster_Pattern.Stand;

    }

    float DistanceToTarget()
    {
        if (!Target)
            return -1.0f;

        return Vector3.Distance(Target.position, transform.position);
    }

    bool Search(float radius)
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, Vector3.forward, out hit, radius, targetLayer))
        {
            Target = hit.collider.GetComponent<Rigidbody>();
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (gizmo)
        {
            Gizmos.DrawWireSphere(transform.position, IdleStateSearchRadius);
            Gizmos.DrawWireSphere(transform.position, FightSearchRadius);
        }
    }

    void test()
    {
        agent.baseOffset = 1.3f;
    }

    void test2()
    {
        agent.baseOffset = 0;
    }

    bool RotateTowardsTarget()
    {
        if (Target)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 회전 속도 조절

            // 회전이 거의 완료되었는지 확인
            if (Quaternion.Angle(transform.rotation, lookRotation) < 2f)
            {
                return true;
            }
        }
        return false;
    }
}
