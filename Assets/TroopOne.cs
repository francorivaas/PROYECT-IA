using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopOne : MonoBehaviour
{
    public Leader leader;
    public GameObject unitPrefab;
    public int unitCount;
    public List<IBoid> units = new List<IBoid>();

    void Start()
    {
        SpawnUnits();
    }

    void SpawnUnits()
    {
        for (int i = 0; i < unitCount; i++)
        {
            GameObject unit = Instantiate(unitPrefab, GetRandomPosition(), Quaternion.identity);
            IBoid boid = unit.GetComponent<IBoid>();
            if (boid != null)
            {
                units.Add(boid);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // Implementa una lógica para obtener posiciones aleatorias dentro de una zona determinada
        return new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }
}
