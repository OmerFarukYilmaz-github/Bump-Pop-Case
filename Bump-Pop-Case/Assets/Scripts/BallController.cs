using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    #region Variables
    
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float lineLength = 10f;
    [SerializeField] float shootPower = 1f;
    [SerializeField] float stopVelocity = 0.5f;
    public bool isActive = false;

    Rigidbody theRb;
    Vector3 targetPoint;
    GameOver gameOver;
    UIManager uiManager;

    bool isFirstTouch = true;
    Vector3 firstTouchPos;
    Vector3 currentTouchPos;
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

    #endregion

    public void Start()
    {
        currentState = states.idle;
        theRb = GetComponent<Rigidbody>();
        uiManager = FindObjectOfType<UIManager>();
        gameOver = FindObjectOfType<GameOver>();
        AlignTargetPosBeforeShoot();
        GameManager.instance.lastShootPos = this.gameObject.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        DontRunRestOfCodeIfNotNecessary();

        CheckForFirstTouch();

        if (Touchscreen.current.press.isPressed && currentState == states.idle && !isFirstTouch && uiManager.IsGameAreaTouched() )
        {
            currentState = states.aiming;
            //Debug.Log("idle dan aiming e gec");
        }
        else if (currentState == states.aiming && theRb.velocity.magnitude <= stopVelocity && !isFirstTouch)
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

    private void DontRunRestOfCodeIfNotNecessary()
    {
        //her top için calismasina gerek yok. bosa kaynak tuketmesin
        if (!isActive) return;
        // oyun alanýna dokunmadýysa calisma
        if (Touchscreen.current.press.isPressed && !uiManager.IsGameAreaTouched()) return;
        // oyun bitme ekranýysa calisma
        if (gameOver.isGameOver) return;
    }

    private void CheckForFirstTouch()
    {
        // ilk dokunulan noktayý al. sonra dokunulan noktayla ilk nokta arasýndaki farka göre
        // lineRenderer donecek
        if (Touchscreen.current.press.isPressed && isFirstTouch && uiManager.IsGameAreaTouched())
        {
            isFirstTouch = false;
            firstTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            //Debug.Log("ilk týklama "+ firstTouchPos);
        }
    }

    void ShootIfFingerUp()
    {
        if (!Touchscreen.current.press.isPressed)
        {
            //Debug.Log("shoot");
            GameManager.instance.lastShootPos = this.gameObject.transform.position;
            theRb.AddForce(targetPoint * shootPower, ForceMode.Impulse);
            
            lineRenderer.enabled = false;
            currentState = states.moving;
            //Debug.Log("aiming den moving e gec ");

        }
    }

    void StopIfItsSlowDown()
    {
        //Debug.Log("moving");
        // top durdu gibi gorunse de hala minik bir ivmeye sahip olabilir
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
        currentTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        float deltaX;
        deltaX = ((currentTouchPos.x - firstTouchPos.x) / 3f);
        // Debug.Log("deltax: " + deltaX);

           if (deltaX > deltaOfXs)
           {
               isClockWiseRotation = true;
           }
           else if (deltaX < deltaOfXs)
           {
               isClockWiseRotation = false;
           }
           else
           {
               //Debug.Log("rotatede bisey yapma return");
               return;
           }

        deltaOfXs = deltaX;

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