using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraSize : MonoBehaviour
{
    [SerializeField] float StartSize;
    [SerializeField] float EndSize;

    [SerializeField] float Speed;

    private Camera Cam;



    void Start()
    {
        Cam = GetComponent<Camera>();

        Cam.orthographicSize = StartSize;
    }

    void FixedUpdate()
    {
        if(Cam.orthographicSize < EndSize)
        {
            Cam.orthographicSize += Speed;
        }
        else
        {
            Destroy(this);
        }
    }
}
