using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    Transform _origin;
    PlayerModel _target;
    float _time;

    public Pursuit(PlayerModel target, Transform origin, float time)
    {
        _origin = origin;
        _target = target;
        _time = time;
    }

    public Vector3 GetDir()
    {
        Vector3 point = _target.transform.position + _target.GetForward * _target.GetSpeed * _time;
        return (point - _origin.position).normalized;
    }
}
