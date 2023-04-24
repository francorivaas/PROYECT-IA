using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownPursuitState<T> : ClownStateBase<T>
{
    ISteering _avoidance;
    ISteering _action;
    Vector3 dir;

    public ClownPursuitState(ISteering avoidance, ISteering action)
    {
        _avoidance = avoidance;
        _action = action;
        if (_avoidance == null) Debug.Log("FUCK");
    }

    public override void Awake()
    {
        base.Awake();   
    }

    public override void Execute()
    {

        base.Execute();
        Vector3 dirAvoidance = _avoidance.GetDir();
        dir = (_action.GetDir() + dirAvoidance * 2).normalized;

        clown.Move(dir);
        clown.LookDir(dir);
        Debug.Log("Pursuit");
    }

    public override void Sleep()
    {
        base.Sleep();        
    }
}
