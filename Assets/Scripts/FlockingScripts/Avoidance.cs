using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Avoidance : MonoBehaviour, IFlocking
{
    public float personalRange;
    public float multiplier;

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        Vector3 dir = Vector3.zero;
        for (int i = 0; i < boids.Count; i++)
        {
            Vector3 diff = self.Position - boids[i].Position;
            float distance = diff.magnitude;
            if (distance > personalRange) continue;
            dir += diff.normalized * (personalRange - distance);
        }
        return dir.normalized * multiplier;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, personalRange);
    }
}
