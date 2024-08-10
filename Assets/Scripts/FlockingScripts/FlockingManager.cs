using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    [SerializeField] private int maxBoids;
    [SerializeField] private LayerMask maskBoids;

    IBoid self;
    IFlocking[] flockings;
    Collider[] colliders;
    List<IBoid> boids;
    
    //
    private EnemyFlocking flock;
    FSM<FlockingStateEnum> fsm;
    State<FlockingStateEnum> _initState;
    //

    private void Awake()
    {
        self = GetComponent<IBoid>();
        flockings = GetComponents<IFlocking>();
        flock = GetComponent<EnemyFlocking>();
        boids = new List<IBoid>();
        colliders = new Collider[maxBoids];

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
