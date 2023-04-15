using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _origin;
    Transform _target;

    public Seek(Transform origin , Transform target)
    {
        _target = target;
        _origin = origin;
    }

    public virtual Vector3 GetDir()
    {
        return (_target.position - _origin.position).normalized;
    }
}
