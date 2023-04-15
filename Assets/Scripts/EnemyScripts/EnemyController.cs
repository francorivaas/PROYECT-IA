using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class EnemyController : MonoBehaviour
{
      
    EnemyModel model;
    public PlayerModel target;
    public float time;
    ISteering _steering;
    ISteering _obsAvoidance;
    public float angle;
    public float radius;
    public int maxObstacles;
    public LayerMask mask;
    public float avoMultiplier;

    private void Awake()
    {
        model = GetComponent<EnemyModel>();
        InitializeSteering();
    }

    private void Update()
    {
        Vector3 dirAvoidance = _obsAvoidance.GetDir();
        Vector3 dir = (_steering.GetDir() + dirAvoidance * avoMultiplier).normalized;
        model.Move(dir);
        model.LookDir(dir);

        if (model.CheckRange(target.transform) && model.CheckAngle(target.transform) && model.CheckView(target.transform))
        {
            print("dentro");
            model.SetLights(true);
        }
        else
        {
            model.SetLights(false);
            print("no está dentro kj");
        }
    }

    void InitializeSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(target, transform, time);
        var evade = new Evade(target, transform, time);
        _obsAvoidance = new ObstacleAvoidance(transform, radius, mask, maxObstacles, angle);
        _steering = seek;
    }
}
