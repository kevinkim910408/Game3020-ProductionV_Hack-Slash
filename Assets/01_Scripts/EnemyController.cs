using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: EnemyController.cs
/// History: 
/// 2020-10-03  - EnemyController for AI
///             
/// Last Modified: 2020-10-04
/// </summary>

public class EnemyController : MonoBehaviour
{
    #region Variables

    protected StateMachine<EnemyController> stateMachine;
    public StateMachine<EnemyController> StateMachine => stateMachine;

    //enemy view
    public LayerMask targetMask;
    public float viewRadius;
    public Transform target;

    //enemy attack
    public float attackRange;
    #endregion


    void Start()
    {
        stateMachine = new StateMachine<EnemyController>(this, new IdleState());
        stateMachine.AddState(new MoveState());
        stateMachine.AddState(new AttackState());

    }

    void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }

    
    public bool IsAvailableAttack
    {
        get
        {
            if (!target)
            {
                return false;
            }
            float distance = Vector3.Distance(transform.position, target.position);
            return (distance <= attackRange);
        }
    }

     public Transform SearchEnemy()
    {
        target = null;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);
        if(targetInViewRadius.Length > 0)
        {
            target = targetInViewRadius[0].transform;
        }

        return target;
    }

    //for debug only
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}


