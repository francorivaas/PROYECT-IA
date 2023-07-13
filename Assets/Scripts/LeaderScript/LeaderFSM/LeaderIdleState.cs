using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderIdleState<T> : LeaderStateBase<T>
{
    public LeaderIdleState()
    {
        
    }

    public override void Awake()
    {
        base.Awake();
        if (leader.ResetIdleTimer)
        {
            leader.SetIdleTimer(leader.GetIdleTime());
            leader.ResetTimerDone();
        }
        leader.Stop();

    }

    public override void Execute()
    {
        base.Execute();
        leader.RunIdleTimer();
        if(leader.CurrentIdleTimer < 0)
        {
            leader.IdleDone();
        }

    }

    public override void Sleep()
    {
        base.Sleep();
    }
}


