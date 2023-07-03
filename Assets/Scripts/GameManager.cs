using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static bool hasKey;
    [SerializeField] private GameObject keyImage;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void ActivateKeyImage()
    {
        keyImage.SetActive(true);
    }
}
