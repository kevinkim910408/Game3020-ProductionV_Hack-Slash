using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: IdleState.cs
/// History: 
/// 2020-10-03  - Idle state
///             
/// Last Modified: 2020-10-03
/// </summary>
/// 
public class IdleState : State<EnemyController>
{
    private Animator animator;
    private CharacterController controller;
    protected int hashMove = Animator.StringToHash("Move");
    protected int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    
    bool isPatrol = false;
    private float minIdleTime = 0.0f;
    private float maxIdleTime = 3.0f;
    private float idleTime = 0.0f;
    

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>();
    }

    public override void OnEnter()
    {
        // idle state - enemy cannot move
        animator?.SetBool(hashMove, false);
        animator.SetFloat(hashMoveSpeed, 0);
        controller?.Move(Vector3.zero);
    }

    public override void Update(float deltaTime)
    {
        // if searched target
        // change to move state
        Transform enemy = context.SearchEnemy();
        if (enemy)
        {
            if (context.IsAvailableAttack)
            {
                // check attack cool time
                // and transition to attack state
                stateMachine.ChangeState<AttackState>();
            }
            else
            {
                stateMachine.ChangeState<MoveState>();
            }
        }
        
        else if (isPatrol && stateMachine.ElapsedTimeInState > idleTime)
        {

        }
        
    }

    public override void OnExit()
    {
    }
}
