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

    private PlayerModel player;
    private LifeController playerLifeController;

    void Start()
    {
        weapons[gun] = 4;
        weapons[shotgun] = 5;
        weapons[machinegun] = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            playerLifeController = player.GetComponent<LifeController>();
            if (playerLifeController != null)
            {
                int currentLife = playerLifeController.ReturnCurrentLife();

                if (currentLife > 50)
                {
                    weapons[gun] = 10;
                    weapons[shotgun] = 2;
                    weapons[machinegun] = 0;
                }

                else if (currentLife < 50)
                {
                    weapons[gun] = 2;
                    weapons[shotgun] = 3;
                    weapons[machinegun] = 5;
                }

                else if (currentLife < 20)
                {
                    weapons[gun] = 0;
                    weapons[shotgun] = 5;
                    weapons[machinegun] = 10;
                }
            }

            var weapon = MyRandoms.Roulette(weapons);
            print(weapon);
            player.GetComponentInChildren<PlayerWeaponHolder>().SetWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
