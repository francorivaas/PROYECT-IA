using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public override void Shoot(Transform firepoint)
    {
        base.Shoot(firepoint);
        RaycastHit hit;
        if (Physics.Raycast(firepoint.transform.position, firepoint.transform.forward, out hit, range))
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
