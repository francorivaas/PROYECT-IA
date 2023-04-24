using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMoveState<T> : ClownStateBase<T>
{
    T inputIdle;
    T inputAttack;

    public ClownMoveState(T reachedWaypointInput, T seenEnemyInput)
    {
        inputIdle = reachedWaypointInput;
        inputAttack = seenEnemyInput;
    }

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();
        if (clown.IsOnWaypoint)
        {
            clown.GetNextWaypointMark();
        }
        else
        {
            clown.LookDir(clown.waypointObjective.position);
            clown.MoveBetweenWaypoints();
        }
        Debug.Log("Running move");
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
