using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownModel : MonoBehaviour
{
    //public float jumpSpeed;
    public float visionRange;
    public float visionAngle;
    public float maxIdleTime;
    public float maxPursuitTime;
    private Rigidbody body;
    private PlayerModel lastPlayerTouch;
    private Animator animator;
    public PlayerModel target;
    public LayerMask layer;
    private float idleTimer;
    private float pursuitTimer;
    private bool touchPlayer;
    private bool touchFloor;
    private bool lookingAtPlayer;
    public List<Transform> waypoints;
    public List<Nodos> deadEndWaypoints;
    public Nodos startingWaypoint;
    public AgentController agentController;
    private bool startIdle = false;

    public float speed;
    public int waypointMark;
    public int nodeMark;
    public int damage = 10;
    //private bool reverseWaypointTraversal = false;
    public Transform waypointObjective; //=> waypoints[waypointMark];

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    //public void GetNextWaypointMark()
    //{
    //    if (!reverseWaypointTraversal)
    //    {
    //        if(waypoints.Count -1 > waypointMark)
    //        {
    //            waypointMark++;
    //        }
    //        else
    //        {
    //            reverseWaypointTraversal = true;
    //        }
    //    }
    //    else
    //    {
    //        if(waypointMark > 0)
    //        {
    //            waypointMark--;
    //        }
    //        else
    //        {
    //            reverseWaypointTraversal = false;
    //        }
    //    }
    //}

    public void GetNextWaypointMark()
    {
        waypointMark++;
        Debug.Log(waypoints.Count);
        Debug.Log(waypointMark);
        if ((waypoints.Count -1 < waypointMark))
        {
            startIdle = true;
            GetNextNodeWaypoint();
        }
        else
        {
            waypointObjective = waypoints[waypointMark];
        }
        
    }

    public void GetNextNodeWaypoint()
    {
        agentController.startNode = deadEndWaypoints[nodeMark];

        nodeMark++;
        if(deadEndWaypoints.Count <= nodeMark)
        {
            nodeMark = 0;
        }
        
        agentController.goalNode = deadEndWaypoints[nodeMark];
        agentController.BFSRun();
    }


    public float GetRandomTime()
    {
        //genero el timer
        return Random.Range(0, maxIdleTime);
    }

    public void SetTime(float timer)
    {
        //lo seteamos
        this.idleTimer = timer;
    }

    public void SetPursuitTime(float timer)
    {
        pursuitTimer = timer;
    }

    public void RunTimer()
    {
        //lo corremos
        idleTimer -= Time.deltaTime;
    }

    public void RunPursuitTimer()
    {
        pursuitTimer -= Time.deltaTime;
    }

    

    public float CurrentTimer
    {
        set
        {
            idleTimer = value;
        }
        get
        {
            return idleTimer;
        }
    }

    public float CurrentPursuitTimer
    {
        set
        {
            pursuitTimer = value;
        }
        get
        {
            return pursuitTimer;
        }
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

        return Physics.Raycast(transform.position, differenceToTarget, out hit, distanceToTarget);
    }

    public bool LookingAtPlayer()
    {
        if (CheckRange(target.transform) && CheckAngle(target.transform) && CheckView(target.transform))
        {
            lookingAtPlayer = true;
        }

        else
        {
            lookingAtPlayer = false;
        }
        return lookingAtPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            touchPlayer = true;
            lastPlayerTouch = player;
            animator.SetTrigger("Attack");
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
        dir.y = transform.position.y;
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

    public Vector3 GetDirectionToWaypoint()
    {
        return (waypoints[waypointMark].transform.position - transform.position).normalized;
    }

    public void MoveBetweenWaypoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointMark].transform.position, Time.deltaTime * speed);
    }

    public void SetWaypoints(List<Nodos> nodeWaypoints)
    {
        var list = new List<Transform>();

        for(int i = 0; i< nodeWaypoints.Count; i++)
        {
            Transform tran = nodeWaypoints[i].transform;
            list.Add(tran);
        }
        SetWaypoints(list);
    }

    public void SetWaypoints(List<Transform> nodeWaypoints)
    {
        waypointMark = 0;
        if (nodeWaypoints.Count == 0) return;
        waypoints = nodeWaypoints;
        waypointObjective = waypoints[0].transform;
    }

    public bool ReachedWaypoint()
    {
        return (Vector3.Distance(transform.position, waypointObjective.position) < 3f);
    }

    public bool EndOfPath()
    {
        return (waypointMark == waypoints.Count - 1);
    }


    public PlayerModel LastPlayerTouch => lastPlayerTouch;

    public bool IsTouchingFloor => touchFloor;

    public bool IsTouchingPlayer => touchPlayer;

    public bool IsOnWaypoint => ReachedWaypoint();

    public bool IsLookingAtPlayer => lookingAtPlayer;

    public bool IsEndOfPath => EndOfPath();

    public bool IsOnIdleState => startIdle;


    public Vector3 NextWaypointDir => GetDirectionToWaypoint();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange); //se multiplica por
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange); //range para que el vector
    }
}
