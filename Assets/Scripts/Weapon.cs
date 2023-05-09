using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo;
    public int currentAmmo;
    public int damage;
    //public Transform firepoint;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public virtual void Shoot()
    {
        print("shooting");
    }
}
