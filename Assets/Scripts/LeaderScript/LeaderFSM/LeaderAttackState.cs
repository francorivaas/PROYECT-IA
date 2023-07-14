using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderAttackState<T> : LeaderStateBase<T>
{
    public override void Awake()
    {
        base.Awake();
        leader.SetAttackTimer(leader.maxAttackTime);
        
    }

    public override void Execute()
    {
        base.Execute();
        var objective = leader.LastPlayerTouched;

        if (objective != null)
        {
            if (objective.GetComponent<LifeController>() != null)
            {
                objective.GetComponent<LifeController>().TakeDamage(10);

            }
        }
    }
}
