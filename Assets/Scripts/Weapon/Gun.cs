using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public float range;
    public PlayerModel player;

    private void Start()
    {
        
    }

    public override void Shoot()
    {
        base.Shoot();
        print("shoot 1");
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
            Debug.DrawRay(player.transform.position, player.transform.forward * range, Color.green);
        }
    }
}
