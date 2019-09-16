using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    public void TakeAShot()
    {
        StartCoroutine("TakeScreenshotAndSave");

    }

    IEnumerator TakeScreenshotAndSave()
    {
        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";

        //Save image to gallery
        NativeGallery.SaveImageToGallery(imageBytes, "AlbumName", fileName, null);
        yield return new WaitForEndOfFrame();
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }
}

