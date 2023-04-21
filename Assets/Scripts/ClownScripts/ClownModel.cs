using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : MonoBehaviour
{
    public float jumpSpeed;
    public Vector2 jumpRange;
    public float visionRange;
    public float visionAngle;
    public Vector3 initialPosition;
    public float maxTime;
    private Rigidbody body;
    private PlayerModel lastPlayerTouch;
    public PlayerModel target;
    public LayerMask layer;
    private float timer;
    private bool touchPlayer;
    private bool touchFloor;
    private bool lookingAtPlayer;
    private bool reachedWaypoint = false;
    public List<Transform> waypoints;
    public float speed;
    public int waypointMark = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public Vector3 GetJumpDirection()
    {
        var x = Random.Range(-jumpRange.x, jumpRange.x);
        var z = Random.Range(-jumpRange.y, jumpRange.y);
        var position = new Vector3(x, 0, z) + initialPosition;
        return (position - transform.position).normalized;
    }

    public void Jump(Vector3 direction)
    {
        body.AddForce((direction + Vector3.up) * jumpSpeed, ForceMode.Impulse);
    }

    public int GetNextWaypointMark()
    {
        if (waypoints.Count > waypointMark) waypointMark++; 
        else waypointMark = 0;
        return waypointMark;
    }

    public Transform waypointObjective => waypoints[waypointMark];

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

    public bool CheckRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < visionRange;
    }

    public bool CheckAngle(Transform target)
    {
        Vector3 forwardVector = transform.forward;
        Vector3 directionTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(forwardVector, directionTarget);

        return visionAngle / 2 > angleToTarget;
    }

    public bool CheckView(Transform target)
    {
        RaycastHit hit;

        Vector3 difference = target.position - transform.position;
        Vector3 differenceToTarget = difference.normalized;
        float distanceToTarget = difference.magnitude;

        return !Physics.Raycast(transform.position, differenceToTarget, out hit, distanceToTarget, layer);
    }

    private void LookingAtPlayer()
    {
        if (CheckRange(target.transform) && CheckAngle(target.transform) && CheckView(target.transform))
        {
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
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
        Gizmos.DrawWireCube(position, new Vector3(jumpRange.x, 1, jumpRange.y));
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

    public void LookDir(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        transform.forward = dir;
    }

    public void ReachedWaypoint()
    {
        if (Vector3.Distance(transform.position, waypointObjective.position) < 1f)
        {
            reachedWaypoint = true;
        }
        else reachedWaypoint = false;
    }

    public PlayerModel LastPlayerTouch => lastPlayerTouch;

    public bool IsTouchingFloor => touchFloor;

    public bool IsTouchingPlayer => touchPlayer;

    public bool IsOnWaypoint => reachedWaypoint;

    public bool IsLookingAtPlayer => lookingAtPlayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange); //se multiplica por
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange); //range para que el vector
    }
}
