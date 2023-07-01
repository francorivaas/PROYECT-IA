using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate();

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;
    private bool isDead;

    public LifebarController lifebar;

    public event SimpleDelegate OnHit;

    private void Start()
    {
        currentLife = maxLife;
        lifebar.SetMaxHealth(maxLife);
    }

    public void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            OnHit?.Invoke();
            currentLife -= damage;
            lifebar.SetHealth(currentLife);
            CheckLife();
        }
    }

    public int ReturnCurrentLife()
    {
        return currentLife;
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
