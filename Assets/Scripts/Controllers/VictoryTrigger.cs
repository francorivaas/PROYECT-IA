using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject noKeyText;
    private float timeToShowText = 5.0f;
    private float currentTimeToShowText;
    private bool canCount;

    private void Start()
    {
        currentTimeToShowText = timeToShowText;    
    }

    private void Update()
    {
        if (canCount)
        {
            currentTimeToShowText -= Time.deltaTime;
            if (currentTimeToShowText <= 0)
            {
                noKeyText.SetActive(false);
                currentTimeToShowText = timeToShowText;
                canCount= false;
            }
        }
    }
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
            canCount = true;
            noKeyText.SetActive(true);
        }
        else noKeyText.SetActive(false);
    }
}
