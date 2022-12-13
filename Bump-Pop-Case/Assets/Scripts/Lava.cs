using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // top lava duserse yok et, ana top duserse oyunun bitir
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
