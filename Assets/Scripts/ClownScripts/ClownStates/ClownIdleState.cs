using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownIdleState<T> : ClownStateBase<T>
{
    T inputAttack;

    public ClownIdleState(T seenEnemyInput)
    {
        inputAttack = seenEnemyInput;
    }

    public override void Awake()
    {
        base.Awake();
        var timer = clown.GetRandomTime();
        clown.CurrentTimer = timer;
        clown.Stop();
    }

    public override void Execute()
    {
        base.Execute();
        clown.RunTimer();

        if (!clown.IsLookingAtPlayer)
        {
            if (clown.CurrentTimer > 0)
            {
                clown.RunTimer();
            }
        }
    }

    public override void Sleep()
    {
        base.Sleep();
        clown.CurrentTimer = 0;
    }
}
