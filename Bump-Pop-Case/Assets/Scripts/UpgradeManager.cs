using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI incomeLevelText;
    [SerializeField] TextMeshProUGUI incomeCostText;
    [SerializeField] TextMeshProUGUI ballCloneCountLevelText;
    [SerializeField] TextMeshProUGUI ballCloneCountCostText;


    public void Start()
    {
        //income upgrade butonunun ustundeki level yaz�s� ve mailyet
        incomeLevelText.text = DatabaseManager.instance.GetIncomeLevelName();
        incomeCostText.text = DatabaseManager.instance.GetIncomeUpgradeCost().ToString();

        // klonlanacak top say�s� upgrade butonunun ustundeki level yaz�s� ve mailyet
        ballCloneCountLevelText.text = DatabaseManager.instance.GetBall2CloneLevelName();
        ballCloneCountCostText.text = DatabaseManager.instance.GetBall2CloneUpgradeCost().ToString();
    }

    public void IncomeUpgrade()
    {   
        // param�z varsa ve 20. levele, maxa ulasilmadiysa
        if (DatabaseManager.instance.GetMoneyAmount() < DatabaseManager.instance.GetIncomeUpgradeCost()
            || incomeLevelText.text == "LEVEL 20")
        { 
            return; 
        }

        // maliyet her seferinde %40 arts�n ve u� duzenlensin
        float newIncomePerBallCost = Mathf.RoundToInt(System.Convert.ToInt32(incomeCostText.text) * 1.4f);
        DatabaseManager.instance.SetIncomeUpgradeCost(newIncomePerBallCost);

        DatabaseManager.instance.AdjustMoney(DatabaseManager.instance.GetMoneyAmount() - Mathf.RoundToInt(System.Convert.ToInt32(incomeCostText.text)));
        incomeCostText.text = newIncomePerBallCost.ToString();



        // level 1 arts�n ve u� duzenlensin
        int upgradeLevel = System.Convert.ToInt32(incomeLevelText.text.Replace("LEVEL", ""));
        DatabaseManager.instance.SetIncomeLevelName("LEVEL " + (++upgradeLevel));
        incomeLevelText.text = "LEVEL " + upgradeLevel;

        // top bas�na geliri 2 kat artt�r
        DatabaseManager.instance.SetIncomePerBall(DatabaseManager.instance.GetIncomePerBall() * 2f);

    }
    public void BallCloneCountUpgrade()
    {
        // param�z varsa ve 20. levele, maxa ulasilmadiysa
        if (DatabaseManager.instance.GetMoneyAmount() < DatabaseManager.instance.GetBall2CloneUpgradeCost()
            || ballCloneCountLevelText.text == "LEVEL 20")
        { return; }
      

        // maliyet her seferinde %40 arts�n ve u� duzenlensin
        int newBallCloneCountCost = Mathf.RoundToInt(System.Convert.ToInt32(ballCloneCountCostText.text) * 1.4f);
        DatabaseManager.instance.SetBall2CloneUpgradeCost(newBallCloneCountCost); 
        DatabaseManager.instance.AdjustMoney(DatabaseManager.instance.GetMoneyAmount() - Mathf.RoundToInt(System.Convert.ToInt32(ballCloneCountCostText**.text)));

        ballCloneCountCostText.text = newBallCloneCountCost.ToString();

        // level 1 arts�n ve u� duzenlensin
        int upgradeLevel = System.Convert.ToInt32(ballCloneCountLevelText.text.Replace("LEVEL", ""));
        DatabaseManager.instance.SetBall2CloneLevelName("LEVEL " + (++upgradeLevel));
        ballCloneCountLevelText.text = "LEVEL " + upgradeLevel;

        // klonlanacak top sayisi 1 arts�n
        DatabaseManager.instance.SetBallCountToClon(DatabaseManager.instance.GetBallCountToClon() + 1);

    }
}
