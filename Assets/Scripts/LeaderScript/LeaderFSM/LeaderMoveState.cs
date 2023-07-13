using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMoveState<T> : LeaderStateBase<T>
{
    // Start is called before the first frame update
    public LeaderMoveState()
    {
       
    }

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();

        if (leader.IsOnWaypoint)
        {
            leader.GetNextWaypointMark();
        }

        leader.LookDir(leader.NextWaypointDir);
        leader.Move(leader.NextWaypointDir);
        
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
