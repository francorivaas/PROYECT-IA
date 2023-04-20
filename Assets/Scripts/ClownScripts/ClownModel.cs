using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : MonoBehaviour
{
    public float jumpSpeed;
    public Vector2 range;
    public Vector3 initialPosition;
    public float maxTime;
    private Rigidbody body;
    private PlayerModel lastPlayerTouch;
    private float timer;
    private bool touchPlayer;
    private bool touchFloor;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public Vector3 GetJumpDirection()
    {
        var x = Random.Range(-range.x, range.x);
        var z = Random.Range(-range.y, range.y);
        var position = new Vector3(x, 0, z) + initialPosition;
        return (position - transform.position).normalized;
    }

    public void Jump(Vector3 direction)
    {
        body.AddForce((direction + Vector3.up) * jumpSpeed, ForceMode.Impulse);
    }

    public float GetRandomTime()
    {
        //genero el timer
        return Random.Range(0, maxTime);
    }

    public void SetTime(float timer)
    {
        //lo seteamos
        this.timer = timer;
    }

    public void RunTimer()
    {
        //lo corremos
        timer -= Time.deltaTime;
    }

    public float CurrentTimer
    {
        set
        {
            timer = value;
        }
        get
        {
            return timer;
        }
    }

    public void Attack(PlayerModel player)
    {
        if (player != null)
        {
            print("cual");
            Destroy(player.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            touchPlayer = true;
            lastPlayerTouch = player;
        }
        else touchFloor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
        if (player == null)
        {
            touchFloor = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = initialPosition;
        if (position == Vector3.zero)
        {
            position = transform.position;
        }
        Gizmos.DrawWireCube(position, new Vector3(range.x, 1, range.y));
    }

    public PlayerModel LastPlayerTouch => lastPlayerTouch;

    public bool IsTouchingFloor => touchFloor;

    public bool IsTouchingPlayer => touchPlayer;

    public void Dead()
    {
        Destroy(gameObject);
    }
}
