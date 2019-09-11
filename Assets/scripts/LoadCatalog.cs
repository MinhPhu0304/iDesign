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

    [SerializeField] GameObject itemListingPrefab;

    public ArrayList loadedItems = new ArrayList();

    GameObject sceneController;

    /*ARSceneController sceneController;*/

    // Start is called before the first frame update
    void Start()
    {
        generateItems();
        sceneController = new GameObject();
        sceneController.AddComponent<ARSceneController>();

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

        /*int itemNo = 0;
        foreach (Item itemInList in loadedItems)
        {
            GameObject itemButton = Instantiate(buttonPrefab);
            itemButton.GetComponent<Button>().GetComponentInChildren<Text>().text = itemInList.GetName();
            itemButton.name = $"{itemInList.GetItemID()} {itemInList.GetName()}";
            itemButton.GetComponent<Button>().onClick.AddListener(() => NavigateToScene(itemInList.GetName()));

            //Set button parent
            itemButton.transform.SetParent(content.transform);

            //Create position
            Vector3 position = new Vector3(0, itemNo * -30-30);
            //Set position to button
            itemButton.transform.position = position;

            itemNo++;
        }*/

        int listingNo = 0;

        foreach (Item itemInList in loadedItems)
        {
            GameObject itemListing = Instantiate(itemListingPrefab);
            itemListing.GetComponentInChildren<Text>().text = itemInList.GetName(); //Set Titletext
            itemListing.name = $"Listing: {itemInList.GetItemID()} {itemInList.GetName()}"; //Set name of gameobject

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ itemInList.GetName()}") as Sprite;
            itemListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnailSprite; //Set thumbnail

            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => NavigateToScene(itemInList.GetName())); //Make previewbutton go to ARScene
            //itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => NavigateToInfoScene(itemInList.GetName())); //Make info button go to info scene

            itemListing.transform.SetParent(content.transform); //Set listing parent
            RectTransform rt = itemListing.GetComponent<RectTransform>(); //get size of listing
            rt.sizeDelta = new Vector2(440, 125); //set size of listing

            Vector3 position = new Vector3(0, listingNo * -135-75); //Create position
            itemListing.transform.position = position; //Set position to listing

            listingNo++;
        }
    }

    private void NavigateToScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.GetComponent<ARSceneController>().ChangeObjectToPlace(selectedObject);

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
