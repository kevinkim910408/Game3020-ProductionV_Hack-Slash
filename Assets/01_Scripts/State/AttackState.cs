using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: AttackState.cs
/// History: 
/// 2020-10-03  - Attack state
///             - Attack animation is not working - need to fix
///             
/// Last Modified: 2020-10-04
/// </summary>

public class AttackState : State<EnemyController>
{
    private Animator animator;

    protected int hashAttack = Animator.StringToHash("Attack");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        if (context.IsAvailableAttack)
        {
            animator?.SetTrigger(hashAttack);
        }
        else
        {
            stateMachine.ChangeState<IdleState>();
        }
    }

    public override void Update(float deltaTime)
    {
    }
}
