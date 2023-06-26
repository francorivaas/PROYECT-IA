using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingMove<T> : FlockingStateBase<T>
{

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();
        if (flockers.IsOnWaypoint)
        {
            flockers.objWaypoint.ActivateDelegate();
        }
        else
        {
            flockers.Move(flockers.GetDir.normalized);
            flockers.LookDirection(flockers.GetDir.normalized);
        }
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
