using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockWaypoint : MonoBehaviour
{
    public event SimpleDelegate OnReachWaypoint;

    public void ActivateDelegate()
    {
        OnReachWaypoint?.Invoke();
    }
}
