using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
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
                enemy.TakeDamage(damage);
            }
        }
        currentAmmo -= 10;
    }
}
