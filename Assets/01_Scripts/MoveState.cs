using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: MoveState.cs
/// History: 
/// 2020-10-03  - Move state
///             
/// Last Modified: 2020-10-04
/// </summary>

public class MoveState : State<EnemyController>
{
    private Animator animator;
    private CharacterController controller;
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("Move");
    private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>();

        agent = context.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        agent?.SetDestination(context.Target.position);
        animator?.SetBool(hashMove, true);
    }

    public override void Update(float deltaTime)
    {
        Transform enemy = context.SearchEnemy();
        if (enemy)
        {
            agent.SetDestination(context.Target.position);
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                controller.Move(agent.velocity * Time.deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, Time.deltaTime);
                return;
            }
        }
        if(!enemy && agent.remainingDistance <= agent.stoppingDistance)
        {
            stateMachine.ChangeState<IdleState>();
        }
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetFloat(hashMoveSpeed, 0.0f);
        agent.ResetPath();
    }
}