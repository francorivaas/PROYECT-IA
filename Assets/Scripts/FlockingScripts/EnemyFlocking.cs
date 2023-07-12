using UnityEngine;

public class EnemyFlocking : MonoBehaviour, IBoid
{
    public float radius;
    public float speed;
    private Rigidbody body;
    public int waypointMark = 0;
    private FlockingManager flockManager;
    public Transform leaderTroop1;
    public Transform leaderTroop2;

    private void Awake()
    {
        flockManager = GetComponent<FlockingManager>();
        body = GetComponent<Rigidbody>();    
    }

    private void Start()
    {
        //for(int i = 0; i < waypoints.Count; i++)
        //{
        //    waypoints[i].OnReachWaypoint += SwitchHivemindObjective;
        //}
    }

    public Vector3 Position => transform.position;

    public Vector3 Front => transform.forward;

    public float Radius => radius;

    public void Move(Vector3 dir)
    {
        dir *= speed;
        dir.y = body.velocity.y;
        body.velocity = dir * speed;
    }

    public void LookDirection(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, radius);
    }

    //public bool ReachedWaypoint()
    //{
    //    //OnReachWaypoint?.Invoke();
    //    if (Vector3.Distance(transform.position, leader.position) < 3f)
    //    {
    //        //OnReachWaypoint?.Invoke();
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public void SwitchHivemindObjective()
    //{
    //    waypointMark++;
    //    if (waypoints.Count - 1 < waypointMark) waypointMark = 0;
    //    objWaypoint = waypoints[waypointMark];
    //}

    //private FlockWaypoint Objective()
    //{
    //    return waypoints[waypointMark];
    //}

    private void OnTriggerEnter(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            if (player.GetComponent<LifeController>() != null)
            {
                player.GetComponent<LifeController>().TakeDamage(2);
            }
        }
    }

    public Transform currentObjective1 => leaderTroop1;
    public Transform currentObjective2 => leaderTroop2;

    //public bool IsOnWaypoint => ReachedWaypoint();

    public Vector3 GetDir => flockManager.Run();
}
