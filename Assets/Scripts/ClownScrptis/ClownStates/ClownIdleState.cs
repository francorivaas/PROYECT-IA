using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownIdleState<T> : ClownStateBase<T>
{
    float randomTime;

    public override void Awake()
    {
        base.Awake();
        randomTime = clown.GetRandomTime();
    }

    public override void Execute()
    {
        base.Execute();
        if (randomTime > 0) 
        {
            randomTime -= Time.deltaTime;
        }
        else
        {

        }
    }
}
