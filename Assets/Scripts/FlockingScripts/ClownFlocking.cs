using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownFlocking : MonoBehaviour, IBoid
{
    public float radius;
    public float speed;
    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();    
    }

    public Vector3 Position => transform.position;

    public Vector3 Front => transform.forward;

    public float Radius => radius;

    public void Move(Vector3 dir)
    {
        dir *= speed;
        dir.y = body.velocity.y;
        body.velocity = dir * speed;
    }

    public void LookDirection(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, radius);
    }
}
