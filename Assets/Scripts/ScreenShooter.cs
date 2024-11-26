using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShooter : MonoBehaviour
{
    public string screenshotFolder = "Screenshots";
    public string screenshotPrefix = "Screenshot";
    public KeyCode screenshotKey = KeyCode.S;
    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        // Create the folder to save screenshots if it doesn't exist
        if (!System.IO.Directory.Exists(screenshotFolder))
        {
            System.IO.Directory.CreateDirectory(screenshotFolder);
        }

        // Define the file path and name
        string screenshotName = $"{screenshotPrefix}_{System.DateTime.Now:yyyy-MM-dd-HH-mm-ss}.png";
        string filePath = System.IO.Path.Combine(screenshotFolder, screenshotName);

        // Capture the screenshot and save it to the defined path
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log($"Screenshot saved at: {filePath}");
    }
}
