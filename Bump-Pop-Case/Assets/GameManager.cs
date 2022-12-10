using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject preGameUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.press.isPressed && preGameUI.activeSelf == true)
        {
            preGameUI.SetActive(false);
        }  
    }

}
