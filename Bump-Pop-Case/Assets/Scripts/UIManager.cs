using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject preGameUI;
    [SerializeField] GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        SetVisibilityOfUI( true, false );
    }

    // Update is called once per frame
    void Update()
    {

        if (Touchscreen.current.press.isPressed && preGameUI.activeSelf == true && IsGameAreaTouched())
        {
            SetVisibilityOfUI(false, true);
        }
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
            Debug.Log("touch over GUI element!");
            return false;
        }
        else
        {
            Debug.Log("touch on gameplay area ");
            return true;
        }
    }
}
