using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    FSM<ClownStateEnum> fsm;
    private ClownModel clown;

    private void Awake()
    {
        clown = GetComponent<ClownModel>();
        InitializedFSM();
    }

    public void InitializedFSM()
    {
        var list = new List<ClownStateBase<ClownStateEnum>>();
        fsm = new FSM<ClownStateEnum>();

        var idle = new ClownIdleState<ClownStateEnum>();
        var jump = new ClownJumpState<ClownStateEnum>();
        var dead = new ClownDiedState<ClownStateEnum>();

        list.Add(idle);
        list.Add(jump);
        list.Add(dead);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(clown, fsm);
        }

        idle.AddTransition(ClownStateEnum.Jump, jump);
        idle.AddTransition(ClownStateEnum.Dead, dead);

        jump.AddTransition(ClownStateEnum.Idle, idle);

        fsm.SetInit(idle);
    }

    public void InitializedTree()
    {
        var idle = new TreeAction(ActionIdle);
        var jump = new TreeAction(ActionJump);
        var dead = new TreeAction(ActionDead);
    }

    private void ActionIdle()
    {
        fsm.Transition(ClownStateEnum.Idle);
    }

    private void ActionJump()
    {
        fsm.Transition(ClownStateEnum.Jump);
    }

    private void ActionDead()
    {
        fsm.Transition(ClownStateEnum.Dead);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }
}
