using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;
    public float timeToDamageAgain = 1.5f;
    public float currentTimeToDamage;
    public bool canDoDamage;
    public bool canStartCounting;

    private void Start()
    {
        currentTimeToDamage = timeToDamageAgain;
        canDoDamage = true;
        canStartCounting = false;
    }

    private void Update()
    {
        if (canStartCounting && !canDoDamage)
        {
            currentTimeToDamage -= Time.deltaTime;
            if (currentTimeToDamage <= 0)
            {
                canDoDamage = true;
                currentTimeToDamage = timeToDamageAgain;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        LifeController player = other.gameObject.GetComponent<LifeController>();
        if (player != null)
        {
            if (canDoDamage)
            {
                player.TakeDamage(damage);
                canDoDamage = false;
                canStartCounting = true;
            }
        }
    }
}
