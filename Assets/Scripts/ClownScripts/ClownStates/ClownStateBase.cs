using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tenemos el estado base porque seteamos una funcion
//que siempre le pasamos lo mismo, el modelo y el fsm
public class ClownStateBase<T> : State<T>
{
    protected ClownModel clown;
    protected FSM<T> fsm;

    public void InitializedState(ClownModel clown, FSM<T> fsm)
    {
        this.clown = clown;
        this.fsm = fsm;
    }
}
