using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = FindObjectOfType<GameOver>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Debug.Log("WIN!!");
            gameOver.Win();
        }
    }


}
