using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public int maxBoids = 5;
    public LayerMask maskBoids;
    IFlocking[] flockings;
    IBoid self;
    Collider[] colliders;
    List<IBoid> boids;

    private void Awake()
    {
        flockings = GetComponents<IFlocking>();
        self = GetComponent<IBoid>();
        colliders = new Collider[maxBoids];
        boids = new List<IBoid>();
    }

    private void Update()
    {
        Run();   
    }

    void Run() //esto debería devolverme un vector, devolver la dirección
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
        self.Move(dir.normalized);
        self.LookDirection(dir.normalized);
    }
}
