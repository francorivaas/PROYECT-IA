using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public GameObject[] units;
    public GameObject unitPrefab;
    public int numUnits;

    private void Start()
    {
        units = new GameObject[numUnits];
        for (int i = 0; i < numUnits; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-transform.position.x, transform.position.x), Random.Range(-transform.position.y, transform.position.y), Random.Range(0,0));
            units[i] = Instantiate(unitPrefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
        }
    }
}
