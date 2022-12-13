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
        //income upgrade butonunun ustundeki level yazýsý ve mailyet
        incomeLevelText.text = DatabaseManager.instance.GetIncomeLevelName();
        incomeCostText.text = DatabaseManager.instance.GetIncomeUpgradeCost().ToString();

        // klonlanacak top sayýsý upgrade butonunun ustundeki level yazýsý ve mailyet
        ballCloneCountLevelText.text = DatabaseManager.instance.GetBall2CloneLevelName();
        ballCloneCountCostText.text = DatabaseManager.instance.GetBall2CloneUpgradeCost().ToString();
    }

    public void IncomeUpgrade()
    {   
        // paramýz varsa ve 20. levele, maxa ulasilmadiysa
        if (DatabaseManager.instance.GetMoneyAmount() < DatabaseManager.instance.GetIncomeUpgradeCost()
            || incomeLevelText.text == "LEVEL 20")
        { 
            return; 
        }

        // maliyet her seferinde %20 artsýn ve uý duzenlensin
        float newIncomePerBallCost = Mathf.RoundToInt(System.Convert.ToInt32(incomeCostText.text) * 1.4f);
        DatabaseManager.instance.SetIncomeUpgradeCost(newIncomePerBallCost);
        incomeCostText.text = newIncomePerBallCost.ToString();

        // level 1 artsýn ve uý duzenlensin
        int upgradeLevel = System.Convert.ToInt32(incomeLevelText.text.Replace("LEVEL", ""));
        DatabaseManager.instance.SetIncomeLevelName("LEVEL " + (++upgradeLevel));
        incomeLevelText.text = "LEVEL " + upgradeLevel;

        // top basýna geliri 1.5 kat arttýr
        DatabaseManager.instance.SetIncomePerBall(DatabaseManager.instance.GetIncomePerBall() * 2f);

    }
    public void BallCloneCountUpgrade()
    {
        // paramýz varsa ve 20. levele, maxa ulasilmadiysa
        if (DatabaseManager.instance.GetMoneyAmount() < DatabaseManager.instance.GetBall2CloneUpgradeCost()
            || ballCloneCountLevelText.text == "LEVEL 20")
        { return; }
      

        // maliyet her seferinde %20 artsýn ve uý duzenlensin
        float newBallCloneCountCost = Mathf.RoundToInt(System.Convert.ToInt32(ballCloneCountCostText.text) * 1.4f);
        DatabaseManager.instance.SetBall2CloneUpgradeCost(newBallCloneCountCost);
        ballCloneCountCostText.text = newBallCloneCountCost.ToString();

        // level 1 artsýn ve uý duzenlensin
        int upgradeLevel = System.Convert.ToInt32(ballCloneCountLevelText.text.Replace("LEVEL", ""));
        DatabaseManager.instance.SetBall2CloneLevelName("LEVEL " + (++upgradeLevel));
        ballCloneCountLevelText.text = "LEVEL " + upgradeLevel;

        // klonlanacak top sayisi 1 artsýn
        DatabaseManager.instance.SetBallCountToClon(DatabaseManager.instance.GetBallCountToClon() + 1);

    }
}
