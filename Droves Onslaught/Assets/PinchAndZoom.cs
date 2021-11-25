using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour
{
    [SerializeField] float MouseZoomSpeed = 15.0f;
    [SerializeField] float TouchZoomSpeed = 0.1f;
    [SerializeField] float ZoomMinBound = 5f;
    [SerializeField] float ZoomMaxBound = 20f;
    private Camera cam;

    [SerializeField] float PanSpeed = 0.1f;


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {

                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom (deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {
            //Scrool mouse wheel to zoom
            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                Zoom(scroll, MouseZoomSpeed);
            }
        }

        //Panning with touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * PanSpeed, -touchDeltaPosition.y * PanSpeed, 0);
        }


        /*
         if(cam.fieldOfView < ZoomMinBound) 
         {
             cam.fieldOfView = 0.1f;
         }
         else
         if(cam.fieldOfView > ZoomMaxBound ) 
         {
             cam.fieldOfView = 179.9f;
         }*/
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        cam.orthographicSize -= deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
        Debug.Log("Zoom");
    }
}