using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    FSM<ClownStateEnum> fsm;
    State<ClownStateEnum> _initState;
    private ClownModel clown;
    ITreeNode root;
    public PlayerModel target;
    public float timeAvoidance;
    ISteering _steering;
    ISteering _obsAvoidance;
    public float angle;
    public float radius;
    public int maxObstacles;
    public LayerMask mask;
    public float avoMultiplier;

    private void Awake()
    {
        Debug.Log("Hey");
        clown = GetComponent<ClownModel>();
        
        InitializeSteering();
        InitializedFSM();
        InitializedTree();
    }

    private void Start()
    {
        fsm.SetInit(_initState);
        clown.GetNextNodeWaypoint();
    }

    public void InitializedFSM()
    {
        var list = new List<ClownStateBase<ClownStateEnum>>();
        fsm = new FSM<ClownStateEnum>();

        var idle = new ClownIdleState<ClownStateEnum>(ClownStateEnum.Attack);
        //var jump = new ClownJumpState<ClownStateEnum>();
        var dead = new ClownDiedState<ClownStateEnum>();
        var attack = new ClownAttackState<ClownStateEnum>();
        var move = new ClownMoveState<ClownStateEnum>(ClownStateEnum.Idle, ClownStateEnum.Attack);
        var pursuit = new ClownPursuitState<ClownStateEnum>(_obsAvoidance, _steering);

        list.Add(idle);
        //list.Add(jump);
        list.Add(dead);
        list.Add(attack);
        list.Add(move);
        list.Add(pursuit);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(clown, fsm);
        }

        //idle.AddTransition(ClownStateEnum.Jump, jump);
        idle.AddTransition(ClownStateEnum.Move, move);
        idle.AddTransition(ClownStateEnum.Attack, attack);
        idle.AddTransition(ClownStateEnum.Pursuit, pursuit);

        //jump.AddTransition(ClownStateEnum.Idle, idle);
        //jump.AddTransition(ClownStateEnum.Attack, attack);

        move.AddTransition(ClownStateEnum.Idle, idle);
        move.AddTransition(ClownStateEnum.Attack, attack);
        move.AddTransition(ClownStateEnum.Pursuit, pursuit);

        attack.AddTransition(ClownStateEnum.Pursuit, pursuit);
        attack.AddTransition(ClownStateEnum.Move, move);
        attack.AddTransition(ClownStateEnum.Idle, idle);

        pursuit.AddTransition(ClownStateEnum.Idle, idle);
        pursuit.AddTransition(ClownStateEnum.Move, move);

        _initState = idle;
    }

    public void InitializedTree()
    {
        var idle = new TreeAction(ActionIdle);
        //var jump = new TreeAction(ActionJump);
        //var dead = new TreeAction(ActionDead);
        var attack = new TreeAction(ActionAttack);
        var move = new TreeAction(ActionMove);
        var pursuit = new TreeAction(ActionPursuit);
        

        var isTimeOver = new TreeQuestion(IsTimeOver, move, idle);
        var isEndOfPath = new TreeQuestion(IsEndOfPath, isTimeOver, move);
        var isPursuitOver = new TreeQuestion(IsPursuitTimeOver, move, pursuit);
        //var isTouchingPlayer = new TreeQuestion(IsTouchingPlayer, dead, isTimeOver);
        //var isTouchingFloor = new TreeQuestion(IsTouchingFloor, isTouchingPlayer, isTouchingPlayerToKill);
        //var isTouchingPlayerToKill = new TreeQuestion(IsTouchingPlayer, attack, isLookingAtPlayer);
        var isTouchingPlayer = new TreeQuestion(IsTouchingPlayer, attack, pursuit);
        var hasReachedWaypoint = new TreeQuestion(HasReachedWaypoint, isEndOfPath, move);
        var isLookingAtPlayer = new TreeQuestion(IsLookingAtPlayer, isTouchingPlayer, hasReachedWaypoint);
        var isTakingDamage = new TreeQuestion(IsTakingDamage, pursuit, isLookingAtPlayer);
        
        root = isTakingDamage;
    }

    bool HasReachedWaypoint()
    {
        return clown.IsOnWaypoint;
    }

    bool IsLookingAtPlayer()
    {
        clown.LookingAtPlayer();
        return clown.IsLookingAtPlayer;
    }
        
    bool IsTouchingPlayer()
    {
        
        return clown.IsTouchingPlayer;
    }

    bool IsEndOfPath()
    {
        return clown.IsEndOfPath;
    }

    //bool IsTouchingFloor()
    //{
    //    return clown.IsTouchingFloor;
    //}

    private bool IsTimeOver()
    {
        return clown.CurrentTimer < 0;
    }

    private bool IsTakingDamage()
    {
        return clown.IsTakingDamage;
    }

    private bool IsPursuitTimeOver()
    {
        return clown.CurrentPursuitTimer < 0;
    }

    private void ActionIdle()
    {
        fsm.Transition(ClownStateEnum.Idle);
    }

    //private void ActionJump()
    //{
    //    fsm.Transition(ClownStateEnum.Jump);
    //}

    //private void ActionDead()
    //{
    //    print("dead");
    //    fsm.Transition(ClownStateEnum.Dead);
    //}

    private void ActionAttack()
    {
        fsm.Transition(ClownStateEnum.Attack);
    }
    
    private void ActionMove()
    {
        fsm.Transition(ClownStateEnum.Move);
    }

    private void ActionPursuit()
    {
        fsm.Transition(ClownStateEnum.Pursuit);
    }

    private void Update()
    {
        fsm.OnUpdate();
        root.Execute();
    }

    private void InitializeSteering()
    {
        
        var pursuit = new Pursuit(target, transform, timeAvoidance);
        _obsAvoidance = new ObstacleAvoidance(transform, radius, mask, maxObstacles, angle);
        _steering = pursuit;
    }
}
