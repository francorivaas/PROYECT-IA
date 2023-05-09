using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Dictionary<string, float> dic = new Dictionary<string, float>();
 
    private void Start()
    {
        dic["Crash"] = 100; 
        dic["Drake"] = 45; 
        dic["Sonic"] = 20; 
        dic["Plomero"] = 1; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //var random = MyRandoms.Range(50, 100);
            //print(random.ToString());

            for (int i = 0; i < 100; i++)
            {
                var item = MyRandoms.Roulette(dic);
                print(item);
            }
        }
    }
}
