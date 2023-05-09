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
    [SerializeField]
    private Weapon currentWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetWeapon(currentWeapon);  
    }

    public void SetWeapon(Weapon currentWeapon)
    {
        this.currentWeapon = currentWeapon;
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
            currentWeapon.Shoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            muzzleFlash.SetActive(false);
        }
        
    }

    public void Shoot()
    {
        muzzleFlash.SetActive(true);
        animator.SetTrigger("Shoot");

        //RaycastHit hit;
        //if (Physics.Raycast(firepoint.transform.position, firepoint.transform.forward, out hit, range))
        //{
        //    print(hit.transform.name);
        //    LifeController enemy = hit.transform.GetComponent<LifeController>();
        //    if (enemy != null)
        //    {
        //        enemy.TakeDamage(damage);
        //    }
        //}
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

