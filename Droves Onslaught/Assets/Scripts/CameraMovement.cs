using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera Cam;

    private bool IsClicked = false;
    private Vector3 DragPos;

    void Start()
    {
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            DragPos = Cam.ScreenToWorldPoint(Input.mousePosition);
            IsClicked = true;
        }

        if (Input.GetMouseButton(2) && IsClicked)
        {
            Vector3 diff = DragPos - Cam.ScreenToWorldPoint(Input.mousePosition);
            diff.z = 0.0f;
            Cam.transform.position += diff;
        }
        if (Input.GetMouseButtonUp(2))
        {
            IsClicked = false;
        }
    }
}
