using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public int maxBoids = 5;
    public LayerMask maskBoids;
    private EnemyFlocking flock;
    IFlocking[] flockings;
    IBoid self;
    Collider[] colliders;
    List<IBoid> boids;
    FSM<FlockingStateEnum> fsm;
    State<FlockingStateEnum> _initState;

    private void Awake()
    {
        flock = GetComponent<EnemyFlocking>();
        flockings = GetComponents<IFlocking>();
        self = GetComponent<IBoid>();
        colliders = new Collider[maxBoids];
        boids = new List<IBoid>();

        InitializedFSM();
    }

    private void Start()
    {
        fsm.SetInit(_initState);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public Vector3 Run() //esto debería devolverme un vector, devolver la dirección
               //y llamarla desde un estado. puede ser un steering behaviour
    {
        boids.Clear();

        int count = Physics.OverlapSphereNonAlloc(self.Position, self.Radius, colliders, maskBoids);

        for (int i = 0; i < count; i++)
        {
            var curr = colliders[i];
            var boid = curr.GetComponent<IBoid>();
            if (boid == null || boid == self) continue;
            boids.Add(boid);
        }

        Vector3 dir = Vector3.zero;

        for (int i = 0; i < flockings.Length; i++)
        {
            var currFlock = flockings[i];
            dir += currFlock.GetDir(boids, self);
        }

        return dir;
    }

    public void InitializedFSM()
    {
        var list = new List<FlockingStateBase<FlockingStateEnum>>();
        fsm = new FSM<FlockingStateEnum>();

        var move = new FlockingMove<FlockingStateEnum>();

        list.Add(move);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(flock, fsm);
        }

        _initState = move;
    }
}
