using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region keys
    public static DatabaseManager instance;

    const string LEVEL_NAME_KEY = "level_name";
    const string BALL_COUNT_TO_CLON_KEY = "ball_count_to_clon";
    const string MONEY_KEY = "money_amount";
    const string INCOME_PER_BALL_KEY = "income_per_ball";

    const string INCOME_PER_BALL_LEVEL_KEY = "income_per_ball_level";
    const string INCOME_PER_BALL_UPGRADE_COST_KEY = "income_per_ball_upgrade_Cost";

    const string BALL_COUNT_TO_CLON_LEVEL_KEY = "ball_count_to_clon_level";
    const string BALL_COUNT_TO_CLON_UPGRADE_COST_KEY = "ball_count_to_clon_upgrade_Cost";
    #endregion

    public void Awake()
    {
        instance = this;
        int countDbManager = FindObjectsOfType<DatabaseManager>().Length;    
        if(countDbManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    
    }


    public void SetLevelName(string levelName)
    {
        PlayerPrefs.SetString(LEVEL_NAME_KEY, levelName);
    }
    public string GetLevelName()
    {
        return PlayerPrefs.GetString(LEVEL_NAME_KEY, "LEVEL 1");
    }



    public void AdjustMoney(float money)
    {
        PlayerPrefs.SetFloat(MONEY_KEY,  money);
    }
    public float GetMoneyAmount()
    {
        return PlayerPrefs.GetFloat(MONEY_KEY, 210f);
    }



    #region INCOME - CLONE Upgrades and amounts
    //top basýna gelir yukseltmesi icin level ismi
    public void SetIncomeLevelName(string levelName)
    {
        PlayerPrefs.SetString(INCOME_PER_BALL_LEVEL_KEY, levelName);
    }
    public string GetIncomeLevelName()
    {
        return PlayerPrefs.GetString(INCOME_PER_BALL_LEVEL_KEY, "LEVEL 1");
    }

    //top basýna gelir yukseltmesi icin maliyet
    public void SetIncomeUpgradeCost(float cost)
    {
        PlayerPrefs.SetFloat(INCOME_PER_BALL_UPGRADE_COST_KEY, cost);
    }
    public float GetIncomeUpgradeCost()
    {
        return PlayerPrefs.GetFloat(INCOME_PER_BALL_UPGRADE_COST_KEY, 100f);
    }

    //top basýna gelir 
    public void SetIncomePerBall(float income)
    {
        PlayerPrefs.SetFloat(INCOME_PER_BALL_KEY, income);
    }
    public float GetIncomePerBall()
    {
        return PlayerPrefs.GetFloat(INCOME_PER_BALL_KEY, 0.1f);
    }



    // klonlanacak toplarýn yukseltmesi icin level ismi
    public void SetBall2CloneLevelName(string levelName)
    {
        PlayerPrefs.SetString(BALL_COUNT_TO_CLON_LEVEL_KEY, levelName);
    }
    public string GetBall2CloneLevelName()
    {
        return PlayerPrefs.GetString(BALL_COUNT_TO_CLON_LEVEL_KEY, "LEVEL 1");
    }

    // klonlanacak toplarýn yukseltmesi icin maliyet
    public void SetBall2CloneUpgradeCost(float cost)
    {
        PlayerPrefs.SetFloat(BALL_COUNT_TO_CLON_UPGRADE_COST_KEY, cost);
    }
    public float GetBall2CloneUpgradeCost()
    {
        return PlayerPrefs.GetFloat(BALL_COUNT_TO_CLON_UPGRADE_COST_KEY, 100f);
    }

    // klonlanacak toplarýn sayýsý
    public void SetBallCountToClon(int ballcount)
    {
        PlayerPrefs.SetInt(BALL_COUNT_TO_CLON_KEY, ballcount);
    }
    public int GetBallCountToClon()
    {
        return PlayerPrefs.GetInt(BALL_COUNT_TO_CLON_KEY, 5);
    }


    #endregion

}