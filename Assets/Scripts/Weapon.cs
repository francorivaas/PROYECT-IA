using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo;
    public int usedAmmoPerShoot;
    public int damage;
    //public Transform firepoint;

    private void Start()
    {
    }

    public virtual void Shoot()
    {
        maxAmmo -= usedAmmoPerShoot;
        if (maxAmmo <= 0)
        {
            print("empty ! ! !");
        }
    }
}
