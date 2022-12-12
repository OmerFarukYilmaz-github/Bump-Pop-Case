using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
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
        losePanel.SetActive(false);
        //vurus hakkini arttir
        isGameOver = false;
    }

    public void ShowAd(bool isReviveAd)
    {
        RewardAdd.instance.ShowAd(isReviveAd);
    }

    public void NextLevel()
    {
        string levelName = DatabaseManager.instance.GetLevelName();
        int level =System.Convert.ToInt32(levelName.Replace("LEVEL ",""));

        DatabaseManager.instance.SetLevelName("LEVEL "+ (level+1));

        uiManager.ResetBallCount();

        SceneManager.LoadScene("LEVEL " + (level + 1));
    }

    public void RestartLevel()
    {
        uiManager.ResetBallCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
