using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int shootCount = 3;
    public float income = 0f;


    public void Awake()
    {
        instance = this;
        int countGameManager = FindObjectsOfType<GameManager>().Length;
        if (countGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(DatabaseManager.instance.GetLevelName());
    }



}
