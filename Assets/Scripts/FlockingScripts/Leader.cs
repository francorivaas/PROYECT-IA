using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour, IFlocking
{
    public Transform target;
    public float multiplier;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        return (target.position - self.Position).normalized * multiplier;
    }
}
