using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeScreenshot : MonoBehaviour
{

    [SerializeField]
    GameObject blink;

    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = Application.persistentDataPath + "/" + fileName;
        string myFolderLocation = "storage/emmc/DCIM"; //EXAMPLE OF DIRECTLY ACCESSING A CUSTOM FOLDER OF THE GALLERY
        string myScreenshotLocation = myFolderLocation + fileName;

        if (!System.IO.Directory.Exists(myFolderLocation))
        {
            System.IO.Directory.CreateDirectory(myFolderLocation);
        }

        ScreenCapture.CaptureScreenshot(pathToSave);

        yield return new WaitForSeconds(2);

        System.IO.File.Move(pathToSave, myScreenshotLocation);

        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS BEGUN
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + myScreenshotLocation) });
        objActivity.Call("sendBroadcast", objIntent);
        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS COMPLETE

        Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }

    /*IEnumerator CRSaveScreenshot()
    {
        yield return new WaitForEndOfFrame();
        // string TwoStepScreenshotPath = MobileNativeShare.SaveScreenshot("Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second);
        // Debug.Log("A new screenshot was saved at " + TwoStepScreenshotPath);

        string myFileName = "Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".png";
        string myDefaultLocation = Application.persistentDataPath + "/" + myFileName;
        string myFolderLocation = "/storage/emulated/0/DCIM/Camera/JCB/";  //EXAMPLE OF DIRECTLY ACCESSING A CUSTOM FOLDER OF THE GALLERY
        string myScreenshotLocation = myFolderLocation + myFileName;

        //ENSURE THAT FOLDER LOCATION EXISTS
        if (!System.IO.Directory.Exists(myFolderLocation))
        {
            System.IO.Directory.CreateDirectory(myFolderLocation);
        }

        ScreenCapture.CaptureScreenshot(myFileName);
        //MOVE THE SCREENSHOT WHERE WE WANT IT TO BE STORED

        yield return new WaitForSeconds(1);

        System.IO.File.Move(myDefaultLocation, myScreenshotLocation);

        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS BEGUN
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + myScreenshotLocation) });
        objActivity.Call("sendBroadcast", objIntent);
        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS COMPLETE

    }*/
}
