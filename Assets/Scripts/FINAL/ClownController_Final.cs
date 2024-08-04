using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController_Final : MonoBehaviour
{
    FSM<ClownStateEnum> fsm;
    State<ClownStateEnum> _initState;
    private ClownModel clown;
    public PlayerModel target;
    public float timeAvoidance;
    ISteering _steering;

    private void Awake()
    {
        clown = GetComponent<ClownModel>();
        InitializeSteering();
        InitializedFSM();
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
        var dead = new ClownDiedState<ClownStateEnum>();
        var move = new ClownMoveState<ClownStateEnum>(ClownStateEnum.Idle, ClownStateEnum.Attack);
        var pursuit = new ClownPursuitState<ClownStateEnum>(_steering);

        list.Add(idle);
        list.Add(dead);
        list.Add(move);
        list.Add(pursuit);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(clown, fsm);
        }

        idle.AddTransition(ClownStateEnum.Move, move);
        idle.AddTransition(ClownStateEnum.Pursuit, pursuit);

        move.AddTransition(ClownStateEnum.Idle, idle);
        move.AddTransition(ClownStateEnum.Pursuit, pursuit);

        pursuit.AddTransition(ClownStateEnum.Idle, idle);
        pursuit.AddTransition(ClownStateEnum.Move, move);

        _initState = idle;
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    private void InitializeSteering()
    {
        if (target != null)
        {
            var pursuit = new Pursuit(target, transform, timeAvoidance);
            _steering = pursuit;
        }
    }
}
