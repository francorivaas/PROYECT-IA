using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel_Final : MonoBehaviour
{
    public float visionRange;
    public float visionAngle;
    public float maxIdleTime;
    public float maxPursuitTime;
    private Rigidbody body;
    private PlayerModel lastPlayerTouch;
    private Animator animator;
    public PlayerModel target;
    private float idleTimer;
    private float pursuitTimer;
    private bool touchPlayer;
    private bool lookingAtPlayer;
    public float speed;
    private bool startIdle = false;
    private bool tookDamage = false;
    private LifeController lifeController;
    private float _attackTimer;
    public float attackTimer;

    private void Awake()
    {
        lifeController = GetComponent<LifeController>();
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        /*lifeController.OnHit += OnHit*/;
        SetAttackTimer(attackTimer);
    }

    public void SetTime(float timer)
    {
        this.idleTimer = timer;
    }

    public void SetAttackTimer(float timer)
    {
        _attackTimer = attackTimer;
    }

    public void SetPursuitTime(float timer)
    {
        pursuitTimer = timer;
    }

    public void RunTimer()
    {
        idleTimer -= Time.deltaTime;
    }

    public void RunAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
    }

    public void RunPursuitTimer()
    {
        pursuitTimer -= Time.deltaTime;
    }

    public float CurrentTimer
    {
        set { idleTimer = value; }
        get { return idleTimer; }
    }

    public float CurrentPursuitTimer
    {
        set { pursuitTimer = value; }
        get { return pursuitTimer; }
    }

    public void Attack(PlayerModel player)
    {
        lastPlayerTouch = player;
        if (player != null)
        {
            if (player.GetComponent<LifeController>() != null)
            {
                player.GetComponent<LifeController>().TakeDamage(10);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            touchPlayer = true;
            lastPlayerTouch = player;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = LastPlayerTouch;

        if (player != null)
        {
            if (_attackTimer <= 0)
            {
                if (player.GetComponent<LifeController>() != null)
                {
                    animator.SetTrigger("Attack");
                    player.GetComponent<LifeController>().TakeDamage(10);
                    SetAttackTimer(attackTimer);
                }
            }
            else
            {
                RunAttackTimer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            touchPlayer = true;
            lastPlayerTouch = player;
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void Move(Vector3 dir)
    {
        Vector3 dirSpeed = dir * speed;
        dirSpeed.y = body.velocity.y;
        body.velocity = dirSpeed;
    }

    public void Stop()
    {
        body.velocity = Vector3.zero;
    }

    public void LookDir(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        transform.forward = dir;
    }

    public PlayerModel LastPlayerTouch => lastPlayerTouch;

    public bool IsTouchingPlayer => touchPlayer;

    public bool IsTakingDamage => tookDamage;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
