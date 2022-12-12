using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    public Vector3 linePos;
    public float lineLength;

    bool isFirstTouch=true;
    Vector3 firstTouchPos;
    Vector3 touchPos;
    float z=1f;
    float deltaOfXs = 0f;
    bool isClockWiseRotation;


    public void Start()
    {
        linePos = new Vector3(0f, transform.position.y, z*lineLength);
    }

    // Update is called once per frame
    void Update()
    {
        //  DrawLine();
        if (Touchscreen.current.press.isPressed && isFirstTouch)
        {
            isFirstTouch = false;
            firstTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
          //  Debug.Log("ilk týklama "+ firstTouchPos);
        }

        if (Touchscreen.current.press.isPressed && !isFirstTouch)
        {
            touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            //  Debug.Log("týklanan yer: "+ touchPos);
            //Debug.Log("xde degisim: "+ (touchPos.x - firstTouchPos.x) / 2f);

            deneme();
        }


    }
    /*
      KOORTINAT DUZLEM BOLGELER

       1.|  2.
     ----|----
       4.|  3.

     */
    void deneme()
    {
            float deltaX;
            deltaX = ((touchPos.x - firstTouchPos.x) / 3f);
            //Debug.Log("x: " + deltaX);

            if (deltaX > deltaOfXs)
            {
                deltaOfXs = deltaX;
                isClockWiseRotation = true;
            }
            else if(deltaX < deltaOfXs)
            {
                deltaOfXs = deltaX;
                isClockWiseRotation = false;
            }
            else
            {
                //bisey yapma
                return;
            }



            if (isClockWiseRotation)
            {
               // Debug.Log("clockWise");
                linePos = Quaternion.Euler(0f, 20f * Mathf.Abs(deltaX ) * Time.deltaTime, 0f) * linePos;
            }
            else
            {
                // Debug.Log("Anti clockWise");
                linePos = Quaternion.Euler(0f, -20f * Mathf.Abs(deltaX) * Time.deltaTime, 0f) * linePos;
            }

            
                        Vector3[] positions = { transform.position, linePos };
                        lineRenderer.SetPositions(positions);
                        lineRenderer.enabled = true;
        
    }

}
