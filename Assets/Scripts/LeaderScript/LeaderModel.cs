using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderModel : MonoBehaviour
{
    //public float jumpSpeed;
    public float visionRange;
    public float visionAngle;
    public float maxIdleTime;
    public float maxAttackTime;
    private Rigidbody body;
    private PlayerModel lastObjectiveTouched;
    private Animator animator;
    public PlayerModel target;

    private float idleTimer;
    private float pursuitTimer;
    private bool touchObjective;
    private bool lookingAtObjective;

    public List<Transform> waypoints;
    public List<Nodos> deadEndWaypoints;
    public Nodos startingWaypoint;
    public AgentController agentController;

    private bool startIdle = false;
    private bool stayIdle = false;
    private bool lostFromView = false;
    private LifeController lifeController;
    private float _attackTimer;

    public LayerMask searchMask;
    public float attackTimer;
    public float speed;
    public int waypointMark;
    public int nodeMark;
    public int damage = 10;

    public float avoidanceAngle;
    public float avoidanceRadius;
    public int avoidanceMaxObstacles;
    public LayerMask avoidanceMask;
    public float avoidanceMultiplier;
    public Transform waypointObjective;

    private void Awake()
    {
        lifeController = GetComponent<LifeController>();
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        _attackTimer = 0;
    }

    public void GetNextWaypointMark()
    {
        waypointMark++;
        Debug.Log(waypoints.Count);
        Debug.Log(waypointMark);
        if ((waypoints.Count - 1 < waypointMark))
        {
            startIdle = true;
            stayIdle = true;
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
        if (deadEndWaypoints.Count <= nodeMark)
        {
            nodeMark = 0;
        }

        agentController.goalNode = deadEndWaypoints[nodeMark];
        agentController.AStarRun();
    }

    public void ResumeMovePostPursuit()
    {
        SetStartingPoint(transform);
        agentController.AStarRun();
    }

    public void FollowLastSeenPosition()
    {
        SetStartingPoint(transform);
        SetEndPoint(target.transform);
        agentController.AStarRun();
    }


    private void SetEndPoint(Transform transforms)
    {
        var getNodes = CheckNodesAround(transforms);
        Nodos nodo = getNodes.GetComponent<Nodos>();
        if(nodo != null)
        {
            agentController.goalNode = nodo;
        }
    }

    private void SetStartingPoint(Transform transforms)
    {
        var getNodes = CheckNodesAround(transforms);
        Nodos nodo = getNodes.GetComponent<Nodos>();
        if (nodo != null)
        {
            agentController.startNode = nodo;
        }
    }

    private Collider CheckNodesAround(Transform transforms)
    {
        Collider[] colliderNodos = new Collider[3];

        int obj = -1;
        var cantidadNodos = Physics.OverlapSphereNonAlloc(transforms.position, 16f, colliderNodos, searchMask);
        for (int i = 0; i < cantidadNodos; i++)
        {
            if (colliderNodos[i].GetComponent<Nodos>() != null)
            {
                float resultado = -1;
                var cant = Vector3.Distance(transforms.position, colliderNodos[i].transform.position);
                if (resultado == -1 || resultado > cant)
                {
                    obj = i;
                }
            }
        }
        if (obj != -1)
            return colliderNodos[obj];
        else
        {
            Debug.Log("Null");
            return null;

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
            lookingAtObjective = true;
        }

        else
        {
            lookingAtObjective = false;
        }
        return lookingAtObjective;
    }

    public void Move(Vector3 dir)
    {
        dir = (dir + GetDirAvoidance() * avoidanceMultiplier).normalized;
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

    public Vector3 GetDirAvoidance()
    {
        Collider[] obstacles = new Collider[avoidanceMaxObstacles];
        int countObstacles = Physics.OverlapSphereNonAlloc(transform.position, avoidanceRadius, obstacles, avoidanceMask);
        Vector3 dirToAvoid = Vector3.zero;
        int detectedObstacles = 0;
        for (int i = 0; i < countObstacles; i++)
        {
            Collider currObstacle = obstacles[i];
            Vector3 closestPoint = currObstacle.ClosestPointOnBounds(transform.position);
            Vector3 diffToPoint = closestPoint - transform.position;
            float angleToObstacle = Vector3.Angle(transform.forward, diffToPoint);
            if (angleToObstacle > avoidanceAngle) continue;
            float distance = diffToPoint.magnitude;
            detectedObstacles++;
            dirToAvoid += -(diffToPoint).normalized * (avoidanceRadius - distance);
        }
        if (countObstacles != 0)
            dirToAvoid /= countObstacles;

        return dirToAvoid.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModel objective = other.gameObject.GetComponent<PlayerModel>();
        if (objective != null)
        {
            touchObjective = true;
            lastObjectiveTouched = objective;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerModel objective = other.gameObject.GetComponent<PlayerModel>();
        if (objective != null)
        {
            touchObjective = false;
            lastObjectiveTouched = objective;
        }
    }

    public Vector3 GetDirectionToWaypoint()
    {
        return (waypoints[waypointMark].transform.position - transform.position).normalized;
    }

    public bool ReachedWaypoint()
    {
        return (Vector3.Distance(transform.position, waypointObjective.position) < 2f);
    }

    public void LostFromViewPostPursuit()
    {
        lostFromView = true;
    }

    public void StartingLastSeenSpot()
    {
        lostFromView = false;
    }

    /// 
    /// Timer Management
    /// 

    public float GetIdleTime()
    {
        return Random.Range(1, maxIdleTime);
    }



    public void SetIdleTimer(float timer)
    {
        idleTimer = timer;
    }

    public void SetAttackTimer(float timer)
    {
        _attackTimer = attackTimer;
    }

    public void RunIdleTimer()
    {
        
        idleTimer -= Time.deltaTime;
    }

    public void RunAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
    }



    public float CurrentIdleTimer
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

    public float CurrentAttackTimer
    {
        set
        {
            _attackTimer = value;
        }
        get
        {
            return _attackTimer;
        }
    }

    public void ResetTimerDone()
    {
        startIdle = false;
    }

    public void IdleDone()
    {
        stayIdle = false;
    }
    /// 
    /// Timer Management End
    /// 

    public Vector3 NextWaypointDir => GetDirectionToWaypoint();
    public bool IsOnWaypoint => ReachedWaypoint();
    public bool IsIdleUp => stayIdle;
    public bool ResetIdleTimer => startIdle;
    public bool IsLookingAtObjective => lookingAtObjective;

    public bool IsPostPursuit => lostFromView;

    public bool IsTouchingObjective => touchObjective;

    public PlayerModel LastPlayerTouched => lastObjectiveTouched;
}
