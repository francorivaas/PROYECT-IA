using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    FSM<LeaderStateEnum> fsm;
    State<LeaderStateEnum> _initState;
    private LeaderModel leader;
    ITreeNode root;
    public PlayerModel target;
    public float timeAvoidance;
    ISteering _steering;

    private void Awake()
    {
        leader = GetComponent<LeaderModel>();

        InitializeSteering();
        InitializedFSM();
        InitializedTree();
    }

    private void Start()
    {
        fsm.SetInit(_initState);
        
    }

    public void InitializedFSM()
    {
        var list = new List<LeaderStateBase<LeaderStateEnum>>();
        fsm = new FSM<LeaderStateEnum>();

        var idle = new LeaderIdleState<LeaderStateEnum>();
        var attack = new LeaderAttackState<LeaderStateEnum>();
        var move = new LeaderMoveState<LeaderStateEnum>();
        var pursuit = new LeaderPursuitState<LeaderStateEnum>(_steering);
        //var flee = new LeaderFleeState<LeaderStateEnum>();

        list.Add(idle);
        list.Add(attack);
        list.Add(move);
        list.Add(pursuit);
        //list.Add(flee);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(leader, fsm);
        }

        idle.AddTransition(LeaderStateEnum.Move, move);
        idle.AddTransition(LeaderStateEnum.Pursuit, pursuit);

        attack.AddTransition(LeaderStateEnum.Pursuit, pursuit);

        move.AddTransition(LeaderStateEnum.Idle, idle);
        move.AddTransition(LeaderStateEnum.Pursuit, pursuit);

        pursuit.AddTransition(LeaderStateEnum.Move, move);
        pursuit.AddTransition(LeaderStateEnum.Attack, attack);

        _initState = move;
    }

    public void InitializedTree()
    {
        var idle = new TreeAction(ActionIdle);
        var attack = new TreeAction(ActionAttack);
        var move = new TreeAction(ActionMove);
        var pursuit = new TreeAction(ActionPursuit);

        var isIdleUp = new TreeQuestion(IsIdleUp, idle, move);
        var isAttackTimerOver = new TreeQuestion(IsAttackTimerOver, attack, pursuit);
        var isTouchingObjective = new TreeQuestion(IsTouchingObjective, isAttackTimerOver, pursuit);
        var isLookingAtObjective = new TreeQuestion(IsLookingAtObjective, isTouchingObjective, isIdleUp);
        //var isTakingDamage = new TreeQuestion(IsTakingDamage, pursuit, isLookingAtPlayer);

        root = isLookingAtObjective;
    }


    bool IsLookingAtObjective()
    {
        leader.LookingAtPlayer();
        return leader.IsLookingAtObjective;
    }

    bool IsTouchingObjective()
    {

        return leader.IsTouchingObjective;
    }

    bool IsIdleUp()
    {
        return leader.IsIdleUp;
    }

    private bool IsAttackTimerOver()
    {
        if(leader.CurrentAttackTimer > 0)
        {
            leader.RunAttackTimer();
        }
        return leader.CurrentAttackTimer <= 0;
    }



    //private bool IsPursuitTimeOver()
    //{
    //    return clown.CurrentPursuitTimer < 0;
    //}

    private void ActionIdle()
    {
        fsm.Transition(LeaderStateEnum.Idle);
    }


    private void ActionAttack()
    {
        fsm.Transition(LeaderStateEnum.Attack);
    }

    private void ActionMove()
    {
        fsm.Transition(LeaderStateEnum.Move);
    }

    private void ActionPursuit()
    {
        fsm.Transition(LeaderStateEnum.Pursuit);
    }

    private void Update()
    {
        fsm.OnUpdate();
        root.Execute();
    }

    private void InitializeSteering()
    {

        var pursuit = new Pursuit(target, transform, timeAvoidance);
        _steering = pursuit;
    }
}
