using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ball")
        {
            if (other.GetComponent<BallController>().isActive)
            {
                GameOver gameOver = FindObjectOfType<GameOver>();
                gameOver.Lose();
            }
            Destroy(other.gameObject);

        }
    }
}
