using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleARCore;
using GoogleARCore.Examples.ObjectManipulation;
using System;

public class LoadCatalog : MonoBehaviour
{
    public GameObject categoryListingPrefab;
    public GameObject brandListingPrefab;
    public GameObject designerListingPrefab;
    public GameObject itemListingPrefab;

    public List<Item> loadedItems = new List<Item>();
    public List<string> foundCategories = new List<string>();
    public List<string> foundBrands = new List<string>();
    public List<string> foundDesigners = new List<string>();

    public List<GameObject> itemListings;
    public List<GameObject> categoryListings;
    public List<GameObject> brandListings;
    public List<GameObject> designerListings;
    public List<GameObject> visibleListings;
    public List<GameObject> disabledListings;

    public bool showingItems;

    public ItemManager itemManager;

    GameObject sceneController;
    GameObject itemSceneController;

    // Start is called before the first frame update
    void Start()
    {
        showingItems = false;

        sceneController = new GameObject();
        sceneController.AddComponent<ObjectPlacementManipulator>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();

        GameObject content = GameObject.Find("Content");

        content.GetComponent<RectTransform>().position = new Vector2(0, 0);

        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        loadedItems = itemManager.GetItemList();

        GenerateAllListingFromItemManager();

        int listingNo = 0;
        foreach (Item itemInList in loadedItems)
        {
            GameObject itemListing = Instantiate(itemListingPrefab);
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
            rt.sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().rect.width - 80, 150);

            itemListings.Add(itemListing);
            disabledListings.Add(itemListing);

            listingNo++;
        }

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
            categoryListing.GetComponentInChildren<Text>().fontSize = 30;
            categoryListing.name = $"Category: {category}";

            categoryListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Thumbnails/{foundItemInCategory.GetName()}");
            categoryListing.GetComponent<Button>().onClick.AddListener(() => ChangeContentToCategory(category));

            categoryListing.transform.SetParent(content.transform, false);

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = categoryListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().rect.width - 80, 150);

            categoryListings.Add(categoryListing);
            visibleListings.Add(categoryListing);

            listingNo++;

        }

        UpdateItemManagerModels();

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

    private Item FindItemBrand(Item item, string brand)
    {
        Item found = null;

        if (item.GetBrand().Contains(brand))
        {
            found = item;
        }

        return found;
    }

    //Updates the the item manager models that are able to be placed
    //Currently all available models are shown.
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

    private Item FindItemDesigner(Item item, string designer)
    {
        Item found = null;

        if (item.GetDesigner().Contains(designer))
        {
            found = item;
        }

        return found;
    }

    private void NavigateToARScene(string name)
    {
        Debug.Log($"Loading resource: {name}");
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        //sceneController.GetComponent<ObjectPlacementManipulator>().ChangeObjectToPlace(selectedObject);
        itemManager.ObjectToPlace = selectedObject;
        Debug.Log("Item Manager: " + itemManager.ObjectToPlace);

        SceneManager.LoadScene("ARManipulation");
    }

    public void SearchCatalog()
    {
        GameObject searchPanel = GameObject.Find("Search Text");
        string search = searchPanel.GetComponentInChildren<Text>().text;

        //If no search text input do nothing
        if (search.Length == 0)
        {
            return;
        }

        List<Item> searchResults = new List<Item>();

        foreach (Item foundItem in loadedItems)
        {
            //Check if searched text is a category
            //Search case doesn't need to match and needs to be 3 characters long
            bool foundCategory = false;
            foreach (string category in foundItem.GetCategories())
            {
                if (category.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 && search.Length >= 3)
                {
                    foundCategory = true;
                }
            }

            //Check if search was found in name or category
            if (foundItem.GetName().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 || foundCategory)
            {
                //If item is not already in search results: add item
                if (!searchResults.Contains(foundItem))
                {
                    searchResults.Add(foundItem);
                }
            }

            if (foundItem.GetBrand().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 || foundCategory)
            {
                //If item is not already in search results: add item
                if (!searchResults.Contains(foundItem))
                {
                    searchResults.Add(foundItem);
                }
            }

            if (foundItem.GetDesigner().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 || foundCategory)
            {
                //If item is not already in search results: add item
                if (!searchResults.Contains(foundItem))
                {
                    searchResults.Add(foundItem);
                }
            }

        }

        HideListings();

        foreach (Item toShow in searchResults)
        {
            showListing(toShow);
        }
    }

    //Hides all listings currently visible
    private void HideListings()
    {
        List<GameObject> listingsToHide = new List<GameObject>();

        foreach (GameObject listing in visibleListings)
        {
            listing.SetActive(false);

            disabledListings.Add(listing);
            listingsToHide.Add(listing);
        }

        foreach (GameObject toHide in listingsToHide)
        {
            visibleListings.Remove(toHide);
        }

        UpdateItemManagerModels();

    }

    //Shows listing that is passed in and updates visible/disabled lists
    private void showListing(Item listing)
    {
        GameObject content = GameObject.Find("Content");
        GameObject itemFound = content.transform.Find($"Listing: { listing.GetItemID()} { listing.GetName()}").gameObject;

        itemFound.SetActive(true);

        visibleListings.Add(itemFound);
        disabledListings.Remove(itemFound);
        UpdateItemManagerModels();
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void GenerateAllListingFromItemManager()
    {
        foreach (Item item in loadedItems)
        {

            List<string> itemCategories = item.GetCategories();

            foreach (string category in itemCategories)
            {
                if (!foundCategories.Contains(category))
                {
                    foundCategories.Add(category);
                }
            }

            if (!foundBrands.Contains(item.GetBrand()))
            {
                foundBrands.Add(item.GetBrand());
            }

            if (!foundDesigners.Contains(item.GetDesigner()))
            {
                foundDesigners.Add(item.GetDesigner());
            }

        }

        foundCategories.Sort();
        foundBrands.Sort();
        foundDesigners.Sort();
    }

    public void ChangeContentToCategory(string category)
    {
        GameObject content = GameObject.Find("Content");

        HideListings();

        foreach (Item itemInList in loadedItems)
        {
            if (itemInList.GetCategories().Contains(category))
            {
                showListing(itemInList);
                content.transform.Find($"Listing: {itemInList.GetItemID()} {itemInList.GetName()}").gameObject.SetActive(true);
            }
        }
        showingItems = true;
    }
}
