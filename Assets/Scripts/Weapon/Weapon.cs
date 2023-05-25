using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float range;

    private void Start()
    {
    }

    public virtual void Shoot(Transform firePoint)
    {

    }
}
