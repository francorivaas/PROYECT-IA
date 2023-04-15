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

    private void Awake()
    {
        model = GetComponent<EnemyModel>();
        InitializeSteering();
    }

    private void Update()
    {
        Vector3 dir = _steering.GetDir();
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
        _steering = seek;
    }
}
