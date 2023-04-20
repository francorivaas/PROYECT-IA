using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownJumpState<T> : ClownStateBase<T>
{
    public override void Awake()
    {
        base.Awake();
        Vector3 direction = clown.GetJumpDirection();
        clown.Jump(direction);
    }
}
