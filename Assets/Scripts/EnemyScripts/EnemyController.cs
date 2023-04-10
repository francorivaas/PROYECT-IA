using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class EnemyController : MonoBehaviour
{
    EnemyModel model;
    public Transform target;

    private void Awake()
    {
        model = GetComponent<EnemyModel>();
    }

    private void Update()
    {
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
}
