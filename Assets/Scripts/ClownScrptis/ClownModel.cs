using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : MonoBehaviour
{
    public float jumpSpeed;
    public Vector2 range;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();    
    }

    public Vector3 GetJumpDirection()
    {
        var x = Random.Range(-range.x, range.x);
        var z = Random.Range(-range.y, range.y);
        var position = new Vector3(x, 0, z);
        return (position - transform.position).normalized;
    }

    public void Jump(Vector3 direction)
    {
        body.AddForce(direction * jumpSpeed, ForceMode.Impulse);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
