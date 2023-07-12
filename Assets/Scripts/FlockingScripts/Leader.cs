using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour, IFlocking
{
    public Transform target;
    public EnemyFlocking flock;
    public float multiplier;

    public Transform newTarget;

    public Transform GetTargetOne()
    {
        return flock.currentObjective1;
    }

    public Transform GetTargetTwo()
    {
        return flock.currentObjective1;
    }

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        return (newTarget.position - self.Position).normalized * multiplier;
    }
}
