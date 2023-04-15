using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public float range;
    public float angle = 120;
    public float speed;
    Rigidbody _rb;
    public LayerMask layer;
    //public GameObject lights;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public bool CheckRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < range;
    }

    public bool CheckAngle(Transform target)
    {
        Vector3 forwardVector = -transform.forward;
        Vector3 directionTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(forwardVector, directionTarget);

        return angle / 2 > angleToTarget;
    }

    public bool CheckView(Transform target)
    {
        RaycastHit hit;

        Vector3 difference = target.position - transform.position;
        Vector3 differenceToTarget = difference.normalized;
        float distanceToTarget = difference.magnitude;

        return !Physics.Raycast(transform.position, differenceToTarget, out hit, distanceToTarget, layer);
    }

    public void SetLights(bool v)
    {
        //if (lights.activeInHierarchy != v)
        //{
        //    lights.SetActive(v);
        //}
    }

    public void Move(Vector3 dir)
    {
        Vector3 dirSpeed = dir * speed;
        dirSpeed.y = _rb.velocity.y;
        _rb.velocity = dirSpeed;
    }

    public void LookDir(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        transform.forward = dir;
    }

    public Vector3 GetForward => transform.forward;
    public float GetSpeed => _rb.velocity.magnitude;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * -transform.forward * range); //se multiplica por
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * -transform.forward * range); //range para que el vector
    }
}
