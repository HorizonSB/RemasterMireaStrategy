using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField] private string FileName;
    [SerializeField] private bool doScreenshot;

    void Start()
    {
        InvokeRepeating("doScreenShot", 1f, 2f);
    }

    void doScreenShot()
    {
        if (doScreenshot)
            ScreenCapture.CaptureScreenshot(FileName + ".png");
    }
}
