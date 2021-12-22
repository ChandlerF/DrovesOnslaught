using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
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
            string time = System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/ScreenShots/pic" + time + ".jpg");
            Debug.Log("ScreenShot!");
        }
    }
}
