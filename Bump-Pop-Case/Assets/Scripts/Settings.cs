using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject settingButton;
    [SerializeField] GameObject settingsPanel;
    public void Start()
    {
        SetPanelVisibility(false);
    }

    public void OnOffSettingsPanel()
    {
        if (settingsPanel.activeSelf)
        {
            SetPanelVisibility(false);
        }
        else
        {
            SetPanelVisibility(true);
        }
    }

    void SetPanelVisibility(bool isPanelVisible)
    {
        settingButton.SetActive(!isPanelVisible);
        settingsPanel.SetActive(isPanelVisible);
    }
   
    /*
    public void OnOffSound(bool isMute)
    {

    }
    public void OnOffVibration(bool isVibratinonOn)
    {

    }
    */


}
