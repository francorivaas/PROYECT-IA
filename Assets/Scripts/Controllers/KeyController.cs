using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerModel player = other.gameObject.GetComponent<PlayerModel>();
        if (player != null)
        {
            GameManager.hasKey = true;
            GameManager.instance.ActivateKeyImage();
            Destroy(gameObject);
        }
    }
}
