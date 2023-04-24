using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;
    private bool isDead;

    public LifebarController lifebar;

    private void Start()
    {
        currentLife = maxLife;
        lifebar.SetMaxHealth(maxLife);
    }

    public void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            lifebar.SetHealth(currentLife);
            CheckLife();
        }
    }

    private void CheckLife()
    {
        if (currentLife <= 0 && !isDead)
        {
            Die();
        } 
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
