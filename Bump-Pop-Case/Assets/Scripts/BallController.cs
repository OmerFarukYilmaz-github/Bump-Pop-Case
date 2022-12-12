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
    public bool isActive = false;

     Rigidbody theRb;
    CameraController cameraController;
     Vector3 targetPoint;
    GameOver gameOver;


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
        cameraController = FindObjectOfType<CameraController>();
        gameOver = FindObjectOfType<GameOver>();

        AlignTargetPosBeforeShoot();
    }


    // Update is called once per frame
    void Update()
    {
        //her top için calismasina gerek yok. bosa kaynak tuketmesin
        if (!isActive) return;  
        // oyun alanýna dokunmadýysa calisma
        if (Touchscreen.current.press.isPressed && !uIManager.IsGameAreaTouched()) return;
        if (gameOver.isGameOver) return;

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
            RotateTargetDirection();
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
            
            theRb.AddForce(targetPoint * shootPower, ForceMode.Impulse);
            
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
            AlignTargetPosBeforeShoot();

            currentState = states.idle;
            //Debug.Log("moving den ýdle a gec");


        }
    }
    void AlignTargetPosBeforeShoot()
    {
        targetPoint = new Vector3(0f, transform.position.y -0.5f, z * lineLength);
    }

    void RotateTargetDirection()
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
            targetPoint = Quaternion.Euler(0f, 20f * Mathf.Abs(deltaX) * Time.deltaTime, 0f) * targetPoint;
        }
        else
        {
             //Debug.Log("Anti clockWise");
            targetPoint = Quaternion.Euler(0f, -20f * Mathf.Abs(deltaX) * Time.deltaTime, 0f) * targetPoint;
        }

        Vector3[] positions = 
        { 
            transform.position, 
            new Vector3
            (
                targetPoint.x + transform.position.x ,
                transform.position.y, 
                targetPoint.z + transform.position.z 
            ) 
        };

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

}