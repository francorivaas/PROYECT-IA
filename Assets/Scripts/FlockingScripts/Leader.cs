using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour, IFlocking
{
    private ClownModel target;
    [SerializeField] private float multiplier;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        return (target.transform.position - self.Position).normalized * multiplier;
    }

    public void SetLeader(ClownModel target)
    {
        this.target = target;
    }

    public ClownModel GetTarget()
    {
        return target;
    }

}
