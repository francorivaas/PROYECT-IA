using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    //Troop 1
    public Leader leaderOne;
    public GameObject unitPrefabT1;
    public int unitCountT1;
    public List<IBoid> unitsT1 = new List<IBoid>();

    //Troop 2
    public Leader leaderTwo;
    public GameObject unitPrefabT2;
    public int unitCountT2;
    public List<IBoid> unitsT2 = new List<IBoid>();

    void Start()
    {
        SpawnUnits();
    }

    void SpawnUnits()
    {
        for (int i = 0; i < unitCountT1; i++)
        {
            GameObject unit = Instantiate(unitPrefabT1, GetRandomPosition(), Quaternion.identity);
            IBoid boid = unit.GetComponent<IBoid>();
            if (boid != null)
            {
                unitsT1.Add(boid);
            }
        }

        for (int i = 0; i < unitCountT1; i++)
        {
            GameObject unit = Instantiate(unitPrefabT2, GetRandomPosition(), Quaternion.identity);
            IBoid boid = unit.GetComponent<IBoid>();
            if (boid != null)
            {
                unitsT2.Add(boid);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // Implementa una lógica para obtener posiciones aleatorias dentro de una zona determinada
        return new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }
}
