using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderPursuitState<T> : LeaderStateBase<T>
{
    ISteering _action;

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


    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
