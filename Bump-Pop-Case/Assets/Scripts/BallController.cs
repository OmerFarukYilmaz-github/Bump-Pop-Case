using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float lineLlength = 10f;
    [SerializeField] float shootPower = 1f;
    [SerializeField] float stopVelocity = 0.5f;
    [SerializeField] bool isActive = false;

    Rigidbody theRb;
    Vector3 linePos;
    
    enum states
    {
        aiming,
        moving,
        idle
    };

    states currentState;

    public void Start()
    {
        currentState = states.idle;
        theRb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActive) return;

        if (currentState == states.idle && Touchscreen.current.press.isPressed)
        {
            currentState = states.aiming;
            Debug.Log("aiming");

        }
        else if(currentState == states.aiming && theRb.velocity.magnitude <= stopVelocity)
        {
            DrawLine();
            ShootIfFingerUp();
        }
        else if (currentState == states.moving)
        {
            StopIfItsSlowDown();
        }
        Debug.Log("vmgn: "+ theRb.velocity.magnitude);

    }

    void DrawLine()
    {
        Vector3 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        touchPos.z = Camera.main.farClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPos);

        //Debug.Log("t: " + touchPos + "wp: "+worldPosition);


        linePos = new Vector3(
                                worldPosition.x / 10f,
                                transform.position.y,
                                transform.rotation.y + lineLlength
                             );


        // {merkez nokta  , iþaret edilen yer}
        Vector3[] positions = { transform.position, linePos };

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    void ShootIfFingerUp()
    {
        if (!Touchscreen.current.press.isPressed)
        {

            Debug.Log("shoot");

            theRb.AddForce(linePos * shootPower, ForceMode.VelocityChange);
            lineRenderer.enabled = false;

            currentState = states.moving;
            Debug.Log("moving");

        }
    }

    void StopIfItsSlowDown()
    {
        if (theRb.velocity.magnitude < stopVelocity)
        {
            theRb.velocity = Vector3.zero;
            theRb.angularVelocity = Vector3.zero;

            currentState = states.idle;
            Debug.Log("ýdle");
        }
    }


}