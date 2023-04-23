using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject model;
    public Transform firepoint;
    public float rotationSpeed;
    public float speed;
    public float range;
    public int damage;

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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firepoint.transform.position, firepoint.transform.forward, out hit, range))
        {
            print(hit.transform.name);
            LifeController enemy = hit.transform.GetComponent<LifeController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void LookDirection(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        dir.y = 0; //Sacar una vez que utilizemos Y
        model.transform.forward = Vector3.RotateTowards(model.transform.forward, dir, Time.deltaTime * rotationSpeed, 0f);
    }

    public Vector3 GetForward => transform.forward;
    public float GetSpeed => rb.velocity.magnitude;
}

