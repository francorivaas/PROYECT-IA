using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public ClownModel clown;
    public Nodos goalNode;
    public Nodos startNode;

    BFS<Nodos> _bfs;


    private void Awake()
    {
        _bfs = new BFS<Nodos>();

    }
    public void BFSRun()
    {
        var start = startNode;
        if (start == null) return;

        var path = _bfs.Run(start, Satisfies, GetConections, 500);
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
    
}
