using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToWayPoint : State<EnemyController>
{
    private Animator animator;
    private CharacterController controller;
    private NavMeshAgent agent;

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
        agent = context.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        if (context.targetWaypoint == null)
        {
           context.FindNextWayPoint();
        }

        
        if (context.targetWaypoint)
        {
            agent?.SetDestination(context.targetWaypoint.position);
            animator?.SetBool(hashMove, true);
        }
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
        else
        {
            if(!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance))
            {
                Transform nextDest = context.FindNextWayPoint();
                if (nextDest)
                {
                    agent.SetDestination(nextDest.position);
                }
                stateMachine.ChangeState<IdleState>();
            }
            else
            {
                controller.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);


            }
        }

        /*
        else if (isPatrol && stateMachine.ElapsedTimeInState > idleTime)
        {

        }*/
        
    }

    public override void OnExit()
    {
        animator?.SetBool(hashMove, false);
        agent.ResetPath(); 
    }
}
