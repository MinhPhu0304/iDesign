using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;

public class PopularItemsBehaviour : MonoBehaviour
{
    public GameObject popularItemPrefab;
    public List<Item> itemList;
    public GameObject popularItemPanel;
    public List<GameObject> itemListings;
    public GameObject sceneController;
    public GameObject itemSceneController;
    //Setting the number of popular items to display to 10
    public int MAX_NUMBER_OF_POPULAR_ITENS = 10;
    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Item>();
        itemListings = new List<GameObject>();

        sceneController = new GameObject();
        sceneController.AddComponent<ARSceneController>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();
        queryItems();
        updatePopularItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method will quet the database for the item. When the database is done.
    public void queryItems()
    {
        //Since the table for the items is not done Im just putting in some test data
        itemList.Add(new Item(0, "Chair", 10.00f, "http://www.google.com", "No description set."));
        itemList.Add(new Item(1, "Couch", 100.00f, "http://www.google.com", "No description set."));
        itemList.Add(new Item(2, "Table", 20.00f, "http://www.google.com", "No description set."));
        itemList.Add(new Item(3, "Andy", 0.00f, "http://www.google.com", "Andy the android."));

        //UnityEngine also has a random so specify that it is System's random
        System.Random rand = new System.Random();

        //Setting the number of clicks for testing purposes
        foreach (Item item in itemList)
        {
            item.numberOfClicks = rand.Next(0, 10);
        }
    }

    public void updatePopularItems()
    {
        itemList.Sort();

        int numberOfPopularItems;

        // Check if the item list is less than the max number of popular items
        // If it is greater than the max number of items make it eual the max number
        if (itemList.Count < MAX_NUMBER_OF_POPULAR_ITENS)
        {
            numberOfPopularItems = itemList.Count;
        } else
        {
            numberOfPopularItems = MAX_NUMBER_OF_POPULAR_ITENS;
        }

        GameObject content = GameObject.Find("Content");

        for (int i =0; i < numberOfPopularItems; ++i)
        {
            GameObject itemListing = Instantiate(popularItemPrefab);
            Item itemInList = itemList[i];

            itemListing.SetActive(false); //Not shown when first loaded

            itemListing.GetComponentInChildren<Text>().text = itemInList.GetName(); //Set Titletext
            itemListing.GetComponentInChildren<Text>().fontSize = 30;
            itemListing.name = $"Listing: {itemInList.GetItemID()} {itemInList.GetName()}"; //Set name of gameobject

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ itemInList.GetName()}") as Sprite;
            itemListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnailSprite; //Set thumbnail

            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => NavigateToARScene(itemInList.GetName())); //Make previewbutton go to ARScene
            itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => NavigateToInfoScene(itemInList)); //Make info button go to info scene

            itemListing.transform.SetParent(content.transform, false); //Set listing parent

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = itemListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(375, 400);

            itemListings.Add(itemListing);
            
            showListing(itemInList);
            itemListing.SetActive(true);

        }    
    }

    private void showListing(Item listing)
    {
        Debug.Log($"Looking for: Listing: {listing.GetItemID()} {listing.GetName()}");

        GameObject content = GameObject.Find("Content");
        GameObject toShow = content.transform.Find($"Listing: { listing.GetItemID()} { listing.GetName()}").gameObject;

        toShow.SetActive(true);
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        Debug.Log("Navigate to infoscene");
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void NavigateToARScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.GetComponent<ARSceneController>().ChangeObjectToPlace(selectedObject);

        SceneManager.LoadScene("ARScene");
    }
}
