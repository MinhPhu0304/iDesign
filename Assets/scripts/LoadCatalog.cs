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

    public ItemManager itemManager;

    GameObject sceneController;
    GameObject itemSceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = new GameObject();
        sceneController.AddComponent<ObjectPlacementManipulator>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();

        GameObject content = GameObject.Find("Content");

        content.GetComponent<RectTransform>().position = new Vector2(0, 0);

        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        loadedItems = itemManager.GetItemList();
        GenerateLists();
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

        Debug.Log("Searching for: " + search);

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

        }

        hideListings();

        foreach (Item toShow in searchResults)
        {
            showListing(toShow);
        }
    }

    //Hides all listings currently visible
    private void hideListings()
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
        Debug.Log($"Looking for: Listing: {listing.GetItemID()} {listing.GetName()}");

        GameObject content = GameObject.Find("Content");
        GameObject toShow = content.transform.Find($"Listing: { listing.GetItemID()} { listing.GetName()}").gameObject;

        toShow.SetActive(true);

        visibleListings.Add(toShow);
        disabledListings.Remove(toShow);
        UpdateItemManagerModels();
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        Debug.Log("Navigate to infoscene");
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void GenerateLists()
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

        var result = string.Join(", ", foundCategories.ToArray());
        Debug.Log($"Categories loaded: {result}");
    }

/*    private void GenerateBrands()
    {
        foreach (Item item in loadedItems)
        {
            scanBrand(item);
        }

        foundBrands.Sort();

        var result = string.Join(", ", foundBrands.ToArray());
        Debug.Log($"Brands loaded: {result}");
    }*/

/*    public void scanBrand(Item item)
    {
        List<string> itemBrands = item.GetBrand();

        foreach (string brand in itemBrands)
        {
            if (foundBrands.Contains(brand))
            {
                Debug.Log($"Brand {brand} is already in foundBrands.");
            }
            else
            {
                foundBrands.Add(brand);
            }
        }
    }*/

/*    private void GenerateDesigners()
    {
        foreach (Item item in loadedItems)
        {
            scanDesign(item);
        }

        foundDesigners.Sort();

        var result = string.Join(", ", foundDesigners.ToArray());
        Debug.Log($"Designers loaded: {result}");
    }*/

/*    public void scanDesign(Item item)
    {
        List<string> itemBrands = item.GetBrand();

        foreach (string brand in itemBrands)
        {
            if (foundBrands.Contains(brand))
            {
                Debug.Log($"Brand {brand} is already in foundBrands.");
            }
            else
            {
                foundBrands.Add(brand);
            }
        }
    }*/

    public void ChangeContentToCategory(string category)
    {
        GameObject content = GameObject.Find("Content");

        hideListings();

        foreach (Item itemInList in loadedItems)
        {
            if (itemInList.GetCategories().Contains(category))
            {
                showListing(itemInList);
                content.transform.Find($"Listing: {itemInList.GetItemID()} {itemInList.GetName()}").gameObject.SetActive(true);
            }
        }
    }

    private void ChangeContentToBrand(string brand)
    {
        GameObject content = GameObject.Find("Content");

        hideListings();

        foreach (Item item in loadedItems)
        {
            if (item.GetBrand().Contains(brand))
            {
                showListing(item);
                content.transform.Find($"Listing: {item.GetItemID()} {item.GetName()}").gameObject.SetActive(true);
            }
        }
    }

    private void ChangeContentToDesigner(string designer)
    {
        GameObject content = GameObject.Find("Content");

        hideListings();

        foreach (Item item in loadedItems)
        {
            if (item.GetDesigner().Contains(designer))
            {
                showListing(item);
                content.transform.Find($"Listing: {item.GetItemID()} {item.GetName()}").gameObject.SetActive(true);
            }
        }
    }

    //Implemented to FilterOpen.cs for filter GUI functionality
    public void categoryGenerate(string category)
    {
        ChangeContentToCategory(category);
    }

    public void brandGenerate(string brand)
    {
        ChangeContentToBrand(brand);
    }

    public void designerGenerate(string designer)
    {
        ChangeContentToDesigner(designer);
    }

    public void resetListing()
    {
        hideListings();

        viewCategories();
        viewDesigners();
        viewBrands();

    }
    private void viewDesigners()
    {
        GameObject content = GameObject.Find("Content");
        int listingNo = 0;
        foreach (string designer in foundDesigners)
        {
            GameObject designerListing = Instantiate(designerListingPrefab);

            Item foundItemInDesigners = null;

            for (int i = 0; i < loadedItems.Count && foundItemInDesigners == null; i++)
            {
                foundItemInDesigners = FindItemDesigner((Item)loadedItems[i], designer);
            }

            designerListing.GetComponentInChildren<Text>().text = designer;
            designerListing.GetComponentInChildren<Text>().fontSize = 30;
            designerListing.name = $"Designer: {designer}";

            designerListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Thumbnails/{foundItemInDesigners.GetName()}");
            designerListing.GetComponent<Button>().onClick.AddListener(() => ChangeContentToDesigner(designer));

            designerListing.transform.SetParent(content.transform, false);

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = designerListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().rect.width - 80, 150);

            designerListings.Add(designerListing);
            visibleListings.Add(designerListing);

            listingNo++;
        }
    }

    private void viewBrands()
    {
        GameObject content = GameObject.Find("Content");
        int listingNo = 0;
        foreach (string brand in foundBrands)
        {
            GameObject brandListing = Instantiate(brandListingPrefab);

            Item foundItemInBrands = null;

            for (int i = 0; i < loadedItems.Count && foundItemInBrands == null; i++)
            {
                foundItemInBrands = FindItemBrand((Item)loadedItems[i], brand);
            }

            brandListing.GetComponentInChildren<Text>().text = brand;
            brandListing.GetComponentInChildren<Text>().fontSize = 30;
            brandListing.name = $"Brand: {brand}";

            brandListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Thumbnails/{foundItemInBrands.GetName()}");
            brandListing.GetComponent<Button>().onClick.AddListener(() => ChangeContentToBrand(brand));

            brandListing.transform.SetParent(content.transform, false);

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = brandListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().rect.width - 80, 150);

            brandListings.Add(brandListing);
            visibleListings.Add(brandListing);

            listingNo++;

        }
    }

    private void viewCategories()
    {
        GameObject content = GameObject.Find("Content");
        int listingNo = 0;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
