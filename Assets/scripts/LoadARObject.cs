using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;

public class LoadARObject : MonoBehaviour
{
    GameObject arController;
    public GameObject itemTitle;

    public void getObject()
    {
        arController = new GameObject();
        arController.AddComponent<ARSceneController>();

        GameObject loadedObject = Resources.Load("Models/" + itemTitle.GetComponent<Text>().text) as GameObject;
        arController.GetComponent<ARSceneController>().ChangeObjectToPlace(loadedObject);
        Debug.Log(loadedObject);
        SceneManager.LoadScene("ARScene");
        
    }
}
