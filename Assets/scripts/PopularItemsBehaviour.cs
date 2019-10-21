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
    private ItemManager itemManager;

    //Setting the number of popular items to display to 10
    private readonly int MAX_NUMBER_OF_POPULAR_ITEMS = 5;
    private readonly int FONT_SIZE = 30;
    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        itemList = itemManager.GetItemList();
        itemListings = new List<GameObject>();

        sceneController = new GameObject();
        sceneController.AddComponent<ARSceneController>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();
        updatePopularItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePopularItems()
    {
        itemList.Sort();

        int numberOfPopularItems;


        // Check if the item list is less than the max number of popular items
        // If it is greater than the max number of items make it equal the max number
        if (itemList.Count < MAX_NUMBER_OF_POPULAR_ITEMS)
        {
            numberOfPopularItems = itemList.Count;
        } else
        {
            numberOfPopularItems = MAX_NUMBER_OF_POPULAR_ITEMS;
        }
        
        GameObject content = GameObject.Find("Content");

        for (int i =0; i < numberOfPopularItems; ++i)
        {
            GameObject itemListing = Instantiate(popularItemPrefab);
            Item itemInList = itemList[i];

            itemListing.SetActive(false);

            itemListing.GetComponentInChildren<Text>().text = itemInList.GetName(); //Set Titletext
            itemListing.GetComponentInChildren<Text>().fontSize = FONT_SIZE;
            itemListing.name = $"Listing: {itemInList.GetItemID()} {itemInList.GetName()}"; //Set name of gameobject

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ itemInList.GetName()}") as Sprite;
            itemListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnailSprite; //Set thumbnail

            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => NavigateToARScene(itemInList.GetName())); //Make previewbutton go to ARScene
            itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => NavigateToInfoScene(itemInList)); //Make info button go to info scene

            itemListing.transform.SetParent(content.transform, false); //Set listing parent

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = itemListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(400, 390);

            itemListings.Add(itemListing);
            showListing(itemInList);
            itemListing.SetActive(true);
     
        }    
    }

    private void showListing(Item listing)
    {
        GameObject content = GameObject.Find("Content");
        GameObject toShow = content.transform.Find($"Listing: { listing.GetItemID()} { listing.GetName()}").gameObject;

        toShow.SetActive(true);
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        Debug.Log("Navigate to infoscene");
        UpdateItemManagerModels();
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void NavigateToARScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.GetComponent<ARSceneController>().ChangeObjectToPlace(selectedObject);


        UpdateItemManagerModels();
        SceneManager.LoadScene("ARManipulation");
    }

    private void UpdateItemManagerModels()
    {
        List<GameObject> newVisibles = new List<GameObject>();

        foreach (GameObject listing in itemListings)
        {
            string modelName;
            string[] GameObjectName = listing.name.Split(' ');

            if (GameObjectName[0] == "Listing:")
            {
                modelName = GameObjectName[2];
                GameObject itemModel = Resources.Load($"Models/{modelName}") as GameObject;

                newVisibles.Add(itemModel);

            }
            else
            {
                Debug.Log("Listing was not an itemListing");
            }
        }

        itemManager.selectableModels = newVisibles;
    }
}
