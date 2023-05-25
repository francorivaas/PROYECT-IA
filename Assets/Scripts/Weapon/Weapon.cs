using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float range;

    public virtual void Shoot(Transform firePoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        {
            print(hit.transform.name + hit.transform.position);

            LifeController enemy = hit.transform.GetComponent<LifeController>();
            if (enemy != null)
            {
                print("enemy found!");
                enemy.TakeDamage(damage);
            }
            else print("no enemy");
        }
    }
}
