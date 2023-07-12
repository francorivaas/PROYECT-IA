using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    [Header("Clown troop")]
    public GameObject[] cUnits;
    public GameObject clownUnits;
    public int clownUnitsAmount;
    
    [Header("Soldier troop")]
    public GameObject[] sUnits;
    public GameObject soldierUnits;
    public int soldierUnitsAmount;

    private void Start()
    {
        sUnits = new GameObject[soldierUnitsAmount];
        cUnits = new GameObject[clownUnitsAmount];
        for (int i = 0; i < soldierUnitsAmount; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-transform.position.x, transform.position.x), Random.Range(-transform.position.y, transform.position.y), Random.Range(0,0));
            cUnits[i] = Instantiate(clownUnits, this.transform.position + unitPos, Quaternion.identity) as GameObject;
            
            sUnits[i] = Instantiate(soldierUnits, this.transform.position + unitPos, Quaternion.identity) as GameObject;
        }
    }
}
