using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    Dictionary<GameObject, float> weapons = new Dictionary<GameObject, float>();

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private GameObject shotgun;

    [SerializeField]
    private GameObject machinegun;

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
