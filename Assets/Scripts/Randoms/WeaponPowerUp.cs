using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    Dictionary<Weapon, float> weapons = new Dictionary<Weapon, float>();

    [SerializeField]
    private Weapon gun;

    [SerializeField]
    private Weapon shotgun;

    [SerializeField]
    private Weapon machinegun;

    void Start()
    {
        weapons[gun] = 4;
        weapons[shotgun] = 5;
        weapons[machinegun] = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            var weapon = MyRandoms.Roulette(weapons);
            print(weapon);
            player.GetComponentInChildren<PlayerWeaponHolder>().SetWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
