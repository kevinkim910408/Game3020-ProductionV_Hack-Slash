﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Student Name: Junho Kim
/// Student Number: 101136986
/// Source File: RandomIdle.cs
/// History: 
/// 2020-09-28  - Randomly run Idle animations
///             
/// Last Modified: 2020-09-28
/// </summary>

public class RandomIdle : StateMachineBehaviour
{
    //state numbers
    [SerializeField]
    int numberOfStates = 2;
    [SerializeField]
    float minNormTime = 0f;
    [SerializeField]
    float maxNormTime = 5f;

    //for calculating

    private float randomNormalTime;

    readonly int hashRandomIdle = Animator.StringToHash("RandomIdle");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Randomly decide a time at which to transition.
        randomNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If transitioning awy from this state reset the random idle parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            animator.SetInteger(hashRandomIdle, -1);
        }

        // If the state is beyond the randomly decided normalised time and not yet transitioning then set a random idle.
        if (stateInfo.normalizedTime > randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRandomIdle, Random.Range(0, numberOfStates));
        }
    }
}
