using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

  //  public int shootCount = 3;
    public float incomeInCurrentPlay = 0f;
    public Vector3 lastShootPos;

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



    void Start()
    {
        //son kalininan leveli ac
        try
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(DatabaseManager.instance.GetLevelName());
        }
        catch
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }



}
