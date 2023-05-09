using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public float range;
    public PlayerModel player;

    public override void Shoot()
    {
        base.Shoot();

        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range))
        {
            print(hit.transform.name);
            LifeController enemy = hit.transform.GetComponent<LifeController>();
            if (enemy != null)
            {
                print("enemy found!");
                enemy.TakeDamage(damage);
            }
        }
        currentAmmo -= 1;
    }
}
