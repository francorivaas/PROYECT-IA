using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingStateBase<T> : State<T>
{
    protected EnemyFlocking flockers;
    protected FSM<T> fsm;

    public void InitializedState(EnemyFlocking flockers, FSM<T> fsm)
    {
        this.flockers = flockers;
        this.fsm = fsm;
    }
}
