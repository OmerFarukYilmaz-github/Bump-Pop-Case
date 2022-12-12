using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject settingButton;
    [SerializeField] GameObject settingsPanel;
    public void Start()
    {
        SetVisibility(false);
    }

    public void OnOffSettingsPanel()
    {
        if (settingsPanel.activeSelf)
        {
            SetVisibility(false);
        }
        else
        {
            SetVisibility(true);
        }
    }

    void SetVisibility(bool isPanelVisible)
    {
        settingButton.SetActive(!isPanelVisible);
        settingsPanel.SetActive(isPanelVisible);
    }
   
    /*
    public void OnOffSound(bool isMute)
    {

    }
    public void OnOffVibration(bool isVibratingOn)
    {

    }
    */


}
