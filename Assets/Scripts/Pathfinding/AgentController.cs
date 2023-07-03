using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public ClownModel clown;
    public Nodos goalNode;
    public Nodos startNode;

    BFS<Nodos> _bfs;
    AStar<Nodos> _aStar;


    private void Awake()
    {
        _bfs = new BFS<Nodos>();
        _aStar = new AStar<Nodos>();

    }
    public void BFSRun()
    {
        var start = startNode;
        if (start == null) return;

        var path = _bfs.Run(start, Satisfies, GetConections, 500);
        clown.SetWaypoints(path);
    }

    public void AStarRun()
    {
       var start = startNode;
        if (start == null) return;

        var path = _aStar.Run(start, Satisfies, GetConections, GetCost, Heuristic, 500);
        clown.SetWaypoints(path);
    }


    List<Nodos> GetConections(Nodos curr)
    {
        return curr.neighbours;
    }
    bool Satisfies(Nodos curr)
    {
        return curr == goalNode;
    }

    float GetCost (Nodos parent, Nodos son)
    {
        int multiplier = 1;
        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, son.transform.position) * multiplier;
        return cost;
    }

    float Heuristic (Nodos curr)
    {
        int multiplier = 10;
        float cost = 0;
        cost += Vector3.Distance(curr.transform.position, goalNode.transform.position) * multiplier;
        return cost;
    }
    
}
