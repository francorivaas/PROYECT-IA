using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownIdleState<T> : ClownStateBase<T>
{
    //T inputMove;
    T inputAttack;

    public ClownIdleState(T seenEnemyInput)
    {
    //    inputMove = timePassedInput;
        inputAttack = seenEnemyInput;
    }

    public override void Awake()
    {
        base.Awake();
        var timer = clown.GetRandomTime();
        clown.CurrentTimer = timer;

    }

    public override void Execute()
    {
        base.Execute();
        clown.RunTimer();

        if (clown.IsLookingAtPlayer)
        {
            fsm.Transition(inputAttack);
        }
        else
        {
            if (clown.CurrentTimer > 0)
            {
                clown.RunTimer();
            }
            else
            {

            }
        }
        
    }

    public override void Sleep()
    {
        base.Sleep();
        clown.CurrentTimer = 0;
        
    }
}
