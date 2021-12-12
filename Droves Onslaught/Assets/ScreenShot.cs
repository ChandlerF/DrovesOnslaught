using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private int num = 0;

    private void Start()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/ScreenShots"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ScreenShots");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/ScreenShots/pic" + num.ToString() + ".jpg");
            num++;
            Debug.Log("ScreenShot!");
        }
    }
}
