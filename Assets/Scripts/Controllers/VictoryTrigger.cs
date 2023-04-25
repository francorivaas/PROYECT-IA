using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerModel>() != null
            && GameManager.hasKey)
        {
            GameManager.instance.LoadGameScene();
        }
        else if (other.gameObject.GetComponent<PlayerModel>() != null 
            && !GameManager.hasKey)
        {
            print("no key");
        }
    }
}
