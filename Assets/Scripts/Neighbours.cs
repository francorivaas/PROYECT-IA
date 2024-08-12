using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbours : MonoBehaviour
{
    [SerializeField] private int _length = 24;
    [SerializeField] private int _width = 40;
    List<Nodos> nodes = new List<Nodos>();

    private void Start()
    {
        nodes.AddRange(GetComponentsInChildren<Nodos>());
        AddNeightbourds(nodes);
    }

    private void AddNeightbourds(List<Nodos> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Nodos currentNode = nodes[i];

            int bottomIndex = i - 1;
            int topIndex = i + 1;
            int rightIndex = i + _length;
            int backIndex = i - _length;

            int totalNodes = _width * _length;

            //Agrego el nodo que esta en la arriba
            if (bottomIndex >= 0 && i % _length != 0)
            {
                currentNode.GetComponent<Nodos>().neighbours.Add(nodes[bottomIndex]);
            }

            //Agrego el nodo que esta abajo
            if (topIndex < totalNodes && topIndex % _length != 0)
            {
                currentNode.GetComponent<Nodos>().neighbours.Add(nodes[topIndex]);
            }

            //Agrego el nodo que esta a la izquierda
            if (rightIndex < totalNodes)
            {
                currentNode.GetComponent<Nodos>().neighbours.Add(nodes[rightIndex]);
            }

            //Agrego el nodo que esta a la derecha
            if (backIndex >= 0)
            {
                currentNode.GetComponent<Nodos>().neighbours.Add(nodes[backIndex]);
            }

        }
    }
}
