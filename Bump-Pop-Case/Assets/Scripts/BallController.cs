using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float lineLength = 10f;
    [SerializeField] float shootPower = 1f;
    [SerializeField] float stopVelocity = 0.5f;
    [SerializeField] bool isActive = false;

    Rigidbody theRb;
   public Vector3 linePos;

    bool isFirstTouch = true;
    Vector3 firstTouchPos;
    Vector3 touchPos;
    float z = 1f;
    float deltaOfXs = 0f;
    bool isClockWiseRotation;

    enum states
    {
        aiming,
        moving,
        idle
    };
    states currentState;

    UIManager uIManager;

    public void Start()
    {
        currentState = states.idle;
        theRb = GetComponent<Rigidbody>();
        uIManager = FindObjectOfType<UIManager>();

        LinePosBeforeShoot();
        
    }


    // Update is called once per frame
    void Update()
    {
        //her top için calismasina gerek yok. bosa kaynak tuketmesin
        if (!isActive) return;  
        // oyun alanýna dokunmadýysa calisma
        if (Touchscreen.current.press.isPressed && !uIManager.IsGameAreaTouched()) return;


        if (Touchscreen.current.press.isPressed && isFirstTouch)
        {
            isFirstTouch = false;
            firstTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            //Debug.Log("ilk týklama "+ firstTouchPos);
        }



        if (Touchscreen.current.press.isPressed && currentState == states.idle && !isFirstTouch)
        {
           //RotateAimToDirection();
            currentState = states.aiming;
            //Debug.Log("idle dan aiming e gec");
        }
        else if(currentState == states.aiming && theRb.velocity.magnitude <= stopVelocity && !isFirstTouch)
        {
            //Debug.Log("aiming");
            RotateAimToDirection();
            ShootIfFingerUp();
        }
        else if (currentState == states.moving)
        {
            StopIfItsSlowDown();
        }
        //Debug.Log("vmgn: "+ theRb.velocity.magnitude);
        
    }


    void ShootIfFingerUp()
    {
        if (!Touchscreen.current.press.isPressed)
        {
            //Debug.Log("shoot");
            
            theRb.AddForce(linePos * shootPower, ForceMode.Impulse);
            
            lineRenderer.enabled = false;
            currentState = states.moving;
            //Debug.Log("aiming den moving e gec ");

        }
    }

    void StopIfItsSlowDown()
    {
        //Debug.Log("moving");
        if (theRb.velocity.magnitude < stopVelocity)
        {
            theRb.velocity = Vector3.zero;
            theRb.angularVelocity = Vector3.zero;

            isFirstTouch = true;
            LinePosBeforeShoot();

            currentState = states.idle;
            //Debug.Log("moving den ýdle a gec");
        }
    }
    void LinePosBeforeShoot()
    {
        linePos = new Vector3(0f, transform.position.y, z * lineLength);
    }

    void RotateAimToDirection()
    {
        touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        float deltaX;
        deltaX = ((touchPos.x - firstTouchPos.x) / 3f);
       // Debug.Log("deltax: " + deltaX);

        if (deltaX > deltaOfXs)
        {
            deltaOfXs = deltaX;
            isClockWiseRotation = true;
        }
        else if (deltaX < deltaOfXs)
        {
            deltaOfXs = deltaX;
            isClockWiseRotation = false;
        }
        else
        {
            //Debug.Log("rotatede bisey yapma return");
            return;
        }



        if (isClockWiseRotation)
        {
            //Debug.Log("clockWise");
            linePos = Quaternion.Euler(0f, 20f * Mathf.Abs(deltaX) * Time.deltaTime, 0f) * linePos;
        }
        else
        {
             //Debug.Log("Anti clockWise");
            linePos = Quaternion.Euler(0f, -20f * Mathf.Abs(deltaX) * Time.deltaTime, 0f) * linePos;
        }

        Vector3[] positions = 
        { 
            transform.position, 
            new Vector3
            (
                linePos.x + transform.position.x,
                transform.position.y, 
                linePos.z + transform.position.z
            ) 
        };

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

}