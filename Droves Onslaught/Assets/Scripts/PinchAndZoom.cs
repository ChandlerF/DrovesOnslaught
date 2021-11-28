using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour
{
    [SerializeField] bool CanMove = true;

    [SerializeField] float MouseZoomSpeed = 15.0f;
    [SerializeField] float TouchZoomSpeed = 0.1f;
    [SerializeField] float ZoomMinBound = 5f;
    [SerializeField] float ZoomMaxBound = 20f;
    private Camera cam;

    [SerializeField] float PanSpeed = 0.1f;

    //Pan via click scrollwheel//
    private bool IsClicked = false;
    private Vector3 DragPos;

    private Arrays ListScript;


    void Start()
    {
        cam = GetComponent<Camera>();
        ListScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Arrays>();

        if(cam.orthographicSize < ZoomMinBound)
        {
            cam.orthographicSize = ZoomMinBound;
        }
        else if(cam.orthographicSize > ZoomMaxBound)
        {
            cam.orthographicSize = ZoomMaxBound;
        }
    }

    void Update()
    {
        if (CanMove)
        {
            if (Input.touchCount > 0)
            {
                if (!ListScript.InPlacingBuildingMode)
                {
                    //Panning with touch//
                    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        transform.Translate(-touchDeltaPosition.x * PanSpeed, -touchDeltaPosition.y * PanSpeed, 0);
                    }

                    // Pinch to zoom
                    else if (Input.touchCount == 2)
                    {

                        // get current touch positions
                        Touch tZero = Input.GetTouch(0);
                        Touch tOne = Input.GetTouch(1);
                        // get touch position from the previous frame
                        Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                        Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                        float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                        float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                        // get offset value
                        float deltaDistance = oldTouchDistance - currentTouchDistance;
                        Zoom(deltaDistance, -TouchZoomSpeed);
                    }
                }
            }
            else
            {
                //Scrool mouse wheel to zoom//
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    float scroll = Input.GetAxis("Mouse ScrollWheel");
                    Zoom(scroll, MouseZoomSpeed);
                }


                //Pan By Clicking Scroll Wheel//

                if (Input.GetMouseButtonDown(2))
                {
                    DragPos = cam.ScreenToWorldPoint(Input.mousePosition);
                    IsClicked = true;
                }

                if (Input.GetMouseButton(2) && IsClicked)
                {
                    Vector3 diff = DragPos - cam.ScreenToWorldPoint(Input.mousePosition);
                    diff.z = 0.0f;
                    cam.transform.position += diff;
                }
                if (Input.GetMouseButtonUp(2))
                {
                    IsClicked = false;
                }
            }
        }
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        cam.orthographicSize -= deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
        Debug.Log("Zoom");
    }
}