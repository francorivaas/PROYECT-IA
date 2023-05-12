using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(currentWeapon);
    }

    public void SetWeapon(GameObject currentWeapon)
    {
        this.currentWeapon = currentWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.GetComponent<Weapon>().Shoot();
        }
    }
}
