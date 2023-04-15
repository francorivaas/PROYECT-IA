using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : ISteering
{
    Transform _origin;
    LayerMask _mask;
    float _radius;
    float _angle;
    Collider[] _obstacles;

    public ObstacleAvoidance(Transform origin, float radius, LayerMask mask, int maxObsObjects, float angle)
    {
        _origin = origin;
        _radius = radius;
        _mask = mask;
        _obstacles = new Collider[maxObsObjects];
        _angle = angle;
    }

    public Vector3 GetDir()
    {
        int countObstacles = Physics.OverlapSphereNonAlloc(_origin.position, _radius, _obstacles, _mask);
        Vector3 dirToAvoid = Vector3.zero;
        int detectedObstacles = 0;
        for(int i = 0; i < countObstacles; i++)
        {
            Collider currObstacle = _obstacles[i];
            Vector3 closestPoint = currObstacle.ClosestPointOnBounds(_origin.position);
            Vector3 diffToPoint = closestPoint - _origin.position;
            float angleToObstacle = Vector3.Angle(_origin.forward, diffToPoint);
            if (angleToObstacle > _angle) continue;
            float distance = diffToPoint.magnitude;
            detectedObstacles++;
            dirToAvoid += -(diffToPoint).normalized * (_radius - distance);
        }
        if(countObstacles != 0) 
            dirToAvoid /= countObstacles;

        return dirToAvoid.normalized;
    }
}
