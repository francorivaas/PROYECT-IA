using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class EnemyController : MonoBehaviour
{
    EnemyModel model;
    public Transform target;
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

        if (model.CheckRange(target) && model.CheckAngle(target) && model.CheckView(target))
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
        var seek = new Seek(transform, target);
        _steering = seek;
    }
}
