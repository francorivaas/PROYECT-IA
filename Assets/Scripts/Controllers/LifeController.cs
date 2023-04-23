using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;
    private bool isDead;

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
