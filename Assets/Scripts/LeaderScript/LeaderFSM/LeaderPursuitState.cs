using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderPursuitState<T> : LeaderStateBase<T>
{
    ISteering _action;
    Vector3 dir;

    public LeaderPursuitState(ISteering action)
    {

        _action = action;

    }

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {

        base.Execute();

        dir = _action.GetDir().normalized;

        leader.Move(dir);
        leader.LookDir(dir);

        

    }

    public override void Sleep()
    {
        base.Sleep();
        leader.LostFromViewPostPursuit();
    }
}
