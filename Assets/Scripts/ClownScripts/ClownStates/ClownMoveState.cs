using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMoveState<T> : ClownStateBase<T>
{
    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();
        clown.LookDir(clown.waypointObjective.position);
        clown.Move(clown.waypointObjective.position);
        if (clown.IsOnWaypoint)
        {
            clown.hola();
        }

    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
