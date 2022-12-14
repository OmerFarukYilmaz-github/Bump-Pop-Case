using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject preGameUI;
    [SerializeField] GameObject restartButton;
    [SerializeField] TextMeshProUGUI levelNameTxt;
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] TextMeshProUGUI ballCountTxt;


    float _money;
    int _ballCount;
    string _levelName;



    // Start is called before the first frame update
    void Start()
    {
        SetVisibilityOfUI( true, false );
        AdjustMoney(DatabaseManager.instance.GetMoneyAmount());
        AdjustBallCount(1);
        _money = DatabaseManager.instance.GetMoneyAmount();
        _ballCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.press.isPressed && preGameUI.activeSelf == true && IsGameAreaTouched())
        {
            SetVisibilityOfUI(false, true);
        }

        moneyTxt.text = DatabaseManager.instance.GetMoneyAmount().ToString();
    }

    void SetVisibilityOfUI(bool isPreGameUIVisibile, bool isrestartButtonVisibile)
    {
        preGameUI.SetActive(isPreGameUIVisibile);
        restartButton.SetActive(isrestartButtonVisibile);
    }

    public bool IsGameAreaTouched()
    {
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<CanvasRenderer>() != null)
        {
            //Debug.Log("touch over GUI element!");
            return false;
        }
        else
        {
            //Debug.Log("touch on gameplay area ");
            return true;
        }
    }

    public void AdjustMoney(float money)

    {
        _money += money;
        moneyTxt.text = _money.ToString("n2");
        DatabaseManager.instance.AdjustMoney(_money);
    }

    public void AdjustBallCount(int ballCount)
    {
        this._ballCount += ballCount;
        ballCountTxt.text = this._ballCount.ToString();
    }
    public void ResetBallCount()
    {
        this._ballCount = 1;
        ballCountTxt.text = this._ballCount.ToString();
    }

}
