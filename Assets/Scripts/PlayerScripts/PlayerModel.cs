using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject model;
    public Transform firepoint;
    private Animator animator;
    public float rotationSpeed;
    public float speed;
    public float range;
    public int damage;
    public GameObject muzzleFlash;
    private PlayerWeaponHolder playerWeaponHolder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerWeaponHolder = GetComponentInChildren<PlayerWeaponHolder>();
    }

    public void Move(Vector3 direction) //el modelo solo recibe la direcci�n
    {
        Vector3 directionSpeed = direction * speed;
        directionSpeed.y = rb.velocity.y;
        rb.velocity = direction * speed;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerWeaponHolder.CurrentWeapon.GetComponent<Weapon>().Shoot(firepoint);
        }
    }

    public void LookDirection(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        dir.y = 0; //Sacar una vez que utilizemos Y
        model.transform.forward = Vector3.RotateTowards(model.transform.forward, dir, Time.deltaTime * rotationSpeed, 0f);
    }

    public Vector3 GetForward => model.transform.forward;
    public float GetSpeed => rb.velocity.magnitude;
}

