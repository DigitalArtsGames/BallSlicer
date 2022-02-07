using UnityEngine;

// Generate a screenshot and save to disk with the name SomeLevel.png.

public class ScreenShot : MonoBehaviour
{
    public void invisibleButton()
    {
        ScreenCapture.CaptureScreenshot("ScreenShot");
    }
}