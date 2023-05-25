using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    [SerializeField]
    private Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(currentWeapon);
    }

    public void SetWeapon(Weapon currentWeapon)
    {
        this.currentWeapon = currentWeapon;
    }
}
