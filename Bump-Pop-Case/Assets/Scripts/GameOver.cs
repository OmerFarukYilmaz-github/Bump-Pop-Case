using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject ballPrefab;

    public bool isGameOver;

    UIManager uiManager;
    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void Win()
    {
        Debug.Log("win");
        isGameOver = true;
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        isGameOver = true;
        Debug.Log("lose");
        losePanel.SetActive(true);
    }

    public void Continue()
    {
        // olmeden oncekþ son vurus noktasýný top spawnla
        var ball = Instantiate(ballPrefab, GameManager.instance.lastShootPos, Quaternion.identity);
        ball.GetComponent<BallController>().isActive = true;
        isGameOver = false;
        
        GameObject.FindGameObjectWithTag("losePanel").SetActive(false);
    }

    public void ShowAd(bool isReviveAd)
    {
        AdManager.instance.ShowAd(isReviveAd);
    }

    public void NextLevel()
    {
        
        string levelName = DatabaseManager.instance.GetLevelName();
        int level = System.Convert.ToInt32(levelName.Replace("LEVEL ",""));
        uiManager.ResetBallCount();
        GameManager.instance.incomeInCurrentPlay = 0;

        try
        {
            DatabaseManager.instance.SetLevelName("LEVEL " + (level + 1));
            SceneManager.LoadScene("LEVEL " + (level + 1));
        }
        catch
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel()
    {
        uiManager.ResetBallCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
