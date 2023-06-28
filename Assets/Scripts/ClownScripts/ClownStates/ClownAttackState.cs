using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAttackState<T> : ClownStateBase<T>
{
    public override void Awake()
    {
        base.Awake();
        var player = clown.LastPlayerTouch;
        
        if (player != null)
        {
            if (player.GetComponent<LifeController>() != null)
            {
                player.GetComponent<LifeController>().TakeDamage(10);

            }
        }
    }
}
