using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction) //el modelo solo recibe la dirección
    {
        Vector3 directionSpeed = direction * speed;
        directionSpeed.y = rb.velocity.y;
        rb.velocity = direction * speed;
    }

    public void LookDirection(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        transform.forward = dir;
    }
}

