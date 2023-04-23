using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAttackState<T> : ClownStateBase<T>
{
    public override void Awake()
    {
        base.Awake();
        var player = clown.LastPlayerTouch;
        clown.Attack(player);

    }
}
