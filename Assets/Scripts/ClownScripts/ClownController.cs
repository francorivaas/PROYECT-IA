using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    FSM<ClownStateEnum> fsm;
    private ClownModel clown;
    ITreeNode root;

    private void Awake()
    {
        clown = GetComponent<ClownModel>();
        InitializedFSM();
        InitializedTree();
    }

    public void InitializedFSM()
    {
        var list = new List<ClownStateBase<ClownStateEnum>>();
        fsm = new FSM<ClownStateEnum>();

        var idle = new ClownIdleState<ClownStateEnum>();
        var jump = new ClownJumpState<ClownStateEnum>();
        var dead = new ClownDiedState<ClownStateEnum>();
        var attack = new ClownDiedState<ClownStateEnum>();
        var move = new ClownMoveState<ClownStateEnum>();

        list.Add(idle);
        list.Add(jump);
        list.Add(dead);
        list.Add(attack);
        list.Add(move);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(clown, fsm);
        }

        idle.AddTransition(ClownStateEnum.Jump, jump);
        idle.AddTransition(ClownStateEnum.Move, move);
        idle.AddTransition(ClownStateEnum.Dead, dead);

        jump.AddTransition(ClownStateEnum.Idle, idle);
        jump.AddTransition(ClownStateEnum.Attack, attack);

        move.AddTransition(ClownStateEnum.Idle, idle);
        move.AddTransition(ClownStateEnum.Attack, attack);

        fsm.SetInit(idle);
    }

    public void InitializedTree()
    {
        var idle = new TreeAction(ActionIdle);
        var jump = new TreeAction(ActionJump);
        var dead = new TreeAction(ActionDead);
        var attack = new TreeAction(ActionAttack);
        var move = new TreeAction(ActionMove);

        var isTimeOver = new TreeQuestion(IsTimeOver, jump, idle);
        var isTouchingPlayer = new TreeQuestion(IsTouchingPlayer, dead, isTimeOver);
        var isTouchingPlayerToKill = new TreeQuestion(IsTouchingPlayer, attack, jump);
        var isTouchingFloor = new TreeQuestion(IsTouchingFloor, isTouchingPlayer, isTouchingPlayerToKill);
        var isLookingAtPlayer = new TreeQuestion(IsLookingAtPlayer, attack, move);
        var hasReachedWaypoint = new TreeQuestion(HasReachedWaypoint, idle, isLookingAtPlayer);
        
        root = isTouchingFloor;
    }

    bool HasReachedWaypoint()
    {
        return clown.IsOnWaypoint;
    }

    bool IsLookingAtPlayer()
    {
        return clown.IsLookingAtPlayer;
    }
        
    bool IsTouchingPlayer()
    {
        return clown.IsTouchingPlayer;
    }

    bool IsTouchingFloor()
    {
        return clown.IsTouchingFloor;
    }

    private bool IsTimeOver()
    {
        return clown.CurrentTimer < 0;
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

    private void ActionAttack()
    {
        fsm.Transition(ClownStateEnum.Attack);
    }
    
    private void ActionMove()
    {
        fsm.Transition(ClownStateEnum.Move);
    }

    private void Update()
    {
        fsm.OnUpdate();
        root.Execute();
    }
}
