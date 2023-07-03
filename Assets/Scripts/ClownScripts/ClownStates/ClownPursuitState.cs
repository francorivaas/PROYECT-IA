using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownPursuitState<T> : ClownStateBase<T>
{
    ISteering _action;
    Vector3 dir;

    public ClownPursuitState(ISteering action)
    {
        
        _action = action;
        
    }

    public override void Awake()
    {
        base.Awake();
        var timer = clown.maxPursuitTime;
        clown.CurrentPursuitTimer = timer;
        
    }

    public override void Execute()
    {

        base.Execute();
        
        dir = _action.GetDir().normalized;

        clown.Move(dir);
        clown.LookDir(dir);
        if (!clown.IsLookingAtPlayer)
        {
            if(clown.CurrentPursuitTimer > 0)
            {
                clown.RunPursuitTimer();
            }
            else
            {
                clown.NoLongerCaresOfDamage();
            }
        }
        
    }

    public override void Sleep()
    {
        base.Sleep();
        clown.ResumeMovePostPursuit();
    }
}
