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

    
    public GameObject categoryListingPrefab;
    public GameObject itemListingPrefab;

    public List<Item> loadedItems = new List<Item>();
    public List<string> foundCategories = new List<string>();

    public List<GameObject> itemListings;
    public List<GameObject> categoryListings;

    GameObject sceneController;
    GameObject itemSceneController;

    // Start is called before the first frame update
    void Start()
    {
        GenerateItems();
        GenerateCategories();

        sceneController = new GameObject();
        sceneController.AddComponent<ARSceneController>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();

        GameObject content = GameObject.Find("Content");

        int listingNo = 0;
        foreach (Item itemInList in loadedItems)
        {
            GameObject itemListing = Instantiate(itemListingPrefab);

            itemListing.SetActive(false); //Not shown when first loaded

            itemListing.GetComponentInChildren<Text>().text = itemInList.GetName(); //Set Titletext
            itemListing.name = $"Listing: {itemInList.GetItemID()} {itemInList.GetName()}"; //Set name of gameobject

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ itemInList.GetName()}") as Sprite;
            itemListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnailSprite; //Set thumbnail

            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => NavigateToARScene(itemInList.GetName())); //Make previewbutton go to ARScene
            itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => NavigateToInfoScene(itemInList)); //Make info button go to info scene

            itemListing.transform.SetParent(content.transform); //Set listing parent
            RectTransform rt = itemListing.GetComponent<RectTransform>(); //get size of listing
            rt.sizeDelta = new Vector2(440, 125); //set size of listing

            itemListings.Add(itemListing);

            listingNo++;
        }

        Debug.Log($"List<T>: {itemListings.Count}");


        listingNo = 0;
        foreach (string category in foundCategories)
        {
            GameObject categoryListing = Instantiate(categoryListingPrefab);

            Item foundItemInCategory = null;

            for (int i = 0; i < loadedItems.Count && foundItemInCategory == null; i++)
            {
                foundItemInCategory = FindItemCategory((Item)loadedItems[i], category);
            }         

            categoryListing.GetComponentInChildren<Text>().text = category;
            categoryListing.name = $"Category: {category}";

            categoryListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Thumbnails/{foundItemInCategory.GetName()}");
            categoryListing.GetComponent<Button>().onClick.AddListener(() => ChangeContentToCategory(category));

            categoryListing.transform.SetParent(content.transform);
            RectTransform rt = categoryListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(440, 125);

            categoryListings.Add(categoryListing);

            listingNo++;

        }

    }

    private Item FindItemCategory(Item item, string category)
    {
        Item found = null;

        if (item.GetCategories().Contains(category))
        {
            found = item;
        }

        return found;
    }

    private void NavigateToARScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.GetComponent<ARSceneController>().ChangeObjectToPlace(selectedObject);

        SceneManager.LoadScene("ARScene");
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        Debug.Log("Navigate to infoscene");
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void GenerateItems()
    {
        Item gotItem;

        loadedItems.Add(new Item(0, "Chair", 10.00f, "www.google.com", "No description set."));
        gotItem = (Item)loadedItems[0];
        gotItem.AddCategory(new string[] { "Office", "Chairs", "Desks"});

        loadedItems.Add(new Item(1, "Couch", 100.00f, "www.google.com", "No description set."));
        gotItem = (Item)loadedItems[1];
        gotItem.AddCategory(new string[] { "Living Room", "Couches", "Lounge" });

        loadedItems.Add(new Item(2, "Table", 20.00f, "www.google.com", "No description set."));
        gotItem = (Item)loadedItems[2];
        gotItem.AddCategory(new string[] { "Living Room", "Tables", "Dining Room", "Office" });

        loadedItems.Add(new Item(3, "Andy", 0.00f, "www.google.com", "Andy the android."));
        gotItem = (Item)loadedItems[3];
        gotItem.AddCategory(new string[] { "Google", "Android" });
    }

    private void GenerateCategories()
    {
        foreach (Item item in loadedItems)
        {
            List<string> itemCategories = item.GetCategories();

            foreach (string category in itemCategories)
            {
                if (foundCategories.Contains(category))
                {
                    Debug.Log($"Category {category} is already in foundCategories.");
                }
                else
                {
                    foundCategories.Add(category);
                }
            }
        }

        foundCategories.Sort();

        var result = string.Join(", ", foundCategories.ToArray());
        Debug.Log($"Categories loaded: {result}");
    }

    private void ChangeContentToCategory(string category)
    {
        GameObject content = GameObject.Find("Content");

        foreach (GameObject catListing in categoryListings)
        {
            catListing.SetActive(false);
        }

        foreach (Item itemInList in loadedItems)
        {
            if (itemInList.GetCategories().Contains(category))
            {
                content.transform.Find($"Listing: {itemInList.GetItemID()} {itemInList.GetName()}").gameObject.SetActive(true);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
