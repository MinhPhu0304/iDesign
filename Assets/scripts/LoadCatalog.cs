using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleARCore;
using System;
//using UnityEngine.Experimental.UIElements;

public class LoadCatalog : MonoBehaviour
{
    Resolution[] resolutions;

    string[] CataLogTitle = { "Chair", "Beds", "Couch", "Coffee table", "Dining table", "Coat rack" };

    [SerializeField] GameObject buttonPrefab;

    public ArrayList loadedItems = new ArrayList();

    ARSceneController sceneController = new ARSceneController();


    // Start is called before the first frame update
    void Start()
    {
        generateItems();

        GameObject content = GameObject.Find("Content");
        //resolutions = Screen.resolutions;
        /*for( int i = 0; i < CataLogTitle.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = CataLogTitle[i];
            button.name = CataLogTitle[i] + i.ToString();
            button.GetComponent<Button>().onClick.AddListener(() => NavigateToScene(button.name));
            button.transform.SetParent(content.transform);
            Vector3 position = new Vector3(0 ,i * 30 - 300);
            button.transform.position = position;
        }*/

        int itemNo = 0;
        foreach (Item itemInList in loadedItems)
        {
            GameObject itemButton = Instantiate(buttonPrefab);
            itemButton.GetComponent<Button>().GetComponentInChildren<Text>().text = itemInList.GetName();
            itemButton.name = $"{itemInList.GetItemID()} {itemInList.GetName()}";
            itemButton.GetComponent<Button>().onClick.AddListener(() => NavigateToScene(itemInList.GetName()));

            //Set button parent
            itemButton.transform.SetParent(content.transform);

            //Create position
            Vector3 position = new Vector3(0, itemNo * -30-10);
            //Set position to button
            itemButton.transform.position = position;

            itemNo++;
            

        }
    }

    private void NavigateToScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.ChangeObjectToPlace(selectedObject);

        SceneManager.LoadScene("ARScene");
        
    }


    private void generateItems()
    {
        loadedItems.Add(new Item(0, "Chair", 10.00f, "www.google.com", "No description set."));
        loadedItems.Add(new Item(1, "Couch", 100.00f, "www.google.com", "No description set."));
        loadedItems.Add(new Item(2, "Table", 20.00f, "www.google.com", "No description set."));
        loadedItems.Add(new Item(3, "Andy", 0.00f, "www.google.com", "Andy the android."));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
