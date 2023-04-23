using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int maxLife;
    public int currentLife;
    public bool isDead;

    private void Start()
    {
        currentLife = maxLife;    
    }

    public void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            CheckLife();
        }
    }

    private void CheckLife()
    {
        if (currentLife <= 0)
        {
            Die();
        } 
    }

    private void Die()
    {
        isDead = true;
    }
}
