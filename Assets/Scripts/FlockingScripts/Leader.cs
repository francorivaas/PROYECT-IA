using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour, IFlocking
{
    public Transform target;
    public EnemyFlocking flock;
    public float multiplier;

    public Transform GetTarget()
    {
        return flock.currentObjective;
    }

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        return (GetTarget().position - self.Position).normalized * multiplier;
    }
}
