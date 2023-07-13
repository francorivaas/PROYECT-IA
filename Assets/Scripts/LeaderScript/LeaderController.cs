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
        //var attack = new LeaderAttackState<LeaderStateEnum>();
        var move = new LeaderMoveState<LeaderStateEnum>();
        //var pursuit = new LeaderPursuitState<LeaderStateEnum>(_state);
        //var flee = new LeaderFleeState<LeaderStateEnum>();

        list.Add(idle);
        //list.Add(idle);
        list.Add(move);
        //list.Add(pursuit);
        //list.Add(flee);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(leader, fsm);
        }


        _initState = move;
    }

    public void InitializedTree()
    {
       

        root = null;
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
