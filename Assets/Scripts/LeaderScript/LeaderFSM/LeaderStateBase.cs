using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderStateBase<T> : State<T>
{
    protected LeaderModel leader;
    protected FSM<T> fsm;

    public void InitializedState(LeaderModel leader, FSM<T> fsm)
    {
        this.leader = leader;
        this.fsm = fsm;
    }
}
