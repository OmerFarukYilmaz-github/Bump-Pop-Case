using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class RewardAdd : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] bool isTestMode;
    [SerializeField] TextMeshProUGUI moneyTxt;

    public bool isReviveAd;
    public static RewardAdd instance;
    private GameOver gameOver;

#if UNITY_ANDROID
    string gameId="5071251";
#elif Unity_IOS
    string gameId="5071250";
#endif

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            // Unutma
            Advertisement.Initialize(gameId, isTestMode);
        }
    }
    public void Start()
    {
        gameOver = FindObjectOfType<GameOver>();
    }

    public void ShowAd(bool isReviveAd)
    {
        this.isReviveAd = isReviveAd;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads Error");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                Debug.LogWarning("Reklam bitti");
                if (isReviveAd)
                {
                    gameOver.Continue();
                }
                else
                {
                    DatabaseManager.instance.AdjustMoney(DatabaseManager.instance.GetMoneyAmount() + (GameManager.instance.income * 5f));
                }

                //Odul
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Reklam skip");
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Reklam basarýsiz");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad Ready");
    }

}
