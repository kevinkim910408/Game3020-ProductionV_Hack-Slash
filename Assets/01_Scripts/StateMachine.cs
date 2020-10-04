using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: StateMachine.cs
/// History: 
/// 2020-10-03  - FSM
///             
/// Last Modified: 2020-10-03
/// </summary>

    // new class for statemachine
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine;
        protected T context;

        //constructor
        public State()
        {

        }

        internal void SetStateMachineAndContext(StateMachine<T> stateMachine, T context)
        {
            this.stateMachine = stateMachine;
            this.context = context;

            OnInitialized();    
        }

        /// Called directly after the machine and context are set allowing the state to do any required setup
        public virtual void OnInitialized()
        { }

        public virtual void OnEnter()
        { }

        public virtual void PreUpdate()
        { }

        // have to have this method
        public abstract void Update(float deltaTime);

        public virtual void OnExit()
        { }

    }

//can not fix anymore - sealed 
public sealed class StateMachine<T>
{
    private T context;
    public event Action OnChangedState;

    private State<T> currentState;
    public State<T> CurrentState => currentState;

    private State<T> previousState;
    public State<T> PreviousState => previousState;

    //check the time after change state
    private float elapsedTimeInState = 0.0f;
    public float ElapsedTimeInState => elapsedTimeInState;

    private Dictionary<System.Type, State<T>> states = new Dictionary<Type, State<T>>();
    /*
    private EnemyController enemyController;
    private IdleState idleState;
    */

    public StateMachine(T context, State<T> initialState)
    {
        this.context = context;

        // Setup our initial state
        AddState(initialState);
        currentState = initialState;
        currentState.OnEnter();
    }
    /*
    public StateMachine(EnemyController enemyController, IdleState idleState)
    {
        this.enemyController = enemyController;
        this.idleState = idleState;
    }
    */

    /// Adds the state to the machine
    public void AddState(State<T> state)
    {
        state.SetStateMachineAndContext(this, context);
        states[state.GetType()] = state;
    }

    /// Tick the state machine with the provided delta time
    public void Update(float deltaTime)
    {
        elapsedTimeInState += deltaTime;

        //currentState.PreUpdate();
        currentState.Update(deltaTime);
    }

    /// Changes the current state
    public R ChangeState<R>() where R : State<T>
    {
        // avoid changing to the same state
        var newType = typeof(R);
        if (currentState.GetType() == newType)
        {
            return currentState as R;
        }
        // only call end if we have a currentState
        if (currentState != null)
        {
            currentState.OnExit();
        }
        // swap states and call OnEnter
        previousState = currentState;
        currentState = states[newType];
        currentState.OnEnter();
        elapsedTimeInState = 0.0f;

        return currentState as R;
    }
}
