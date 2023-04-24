using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public GameManager gManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerModel>() != null)
            gManager.LoadGameScene();
    }
}
