using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownDiedState<T> : ClownStateBase<T>
{
    public override void Awake()
    {
        base.Awake();
        clown.Dead();
    }
}
