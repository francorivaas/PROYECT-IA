using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0, 2, 0);
    }
}
