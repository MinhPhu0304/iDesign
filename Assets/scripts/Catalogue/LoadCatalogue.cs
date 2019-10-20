using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Loads and manipulates listing prefabs
public class LoadCatalogue : MonoBehaviour
{
    private ItemManager itemManager;

    public GameObject CategoryListingPrefab;
    public GameObject ItemListingPrefab;
    public GameObject content;

    public List<string> FoundCategories;
    

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        itemManager.ShowingItems = false;

        //If the ItemManager is not currently holding listings
        //Create listings.
        //Otherwise pull listings from the item manager
        if (!itemManager.HoldingListings)
        {
            Debug.Log("Calling to generate item listings");
            GenerateItemListings();
            Debug.Log("Finished item listings");
            GenerateCategoryListings();
        }
        else
        {
            MoveListingObjects(content, false, true);
        }
    }


    //Generates items found in the ItemManagers' loadedItems list.
    //Adds each item to a gameobject that contains the item reference, thumbnail, model and listing
    //All generated item listings are stored in itemManager.ItemReferences
    private void GenerateItemListings()
    {
        Debug.Log("Generating item listings");
        foreach (Item itemInList in itemManager.loadedItems)
        {
            
            GameObject itemListing = Instantiate(ItemListingPrefab);

            ItemReference thisItemReference = itemListing.GetComponent<ItemReference>();

            thisItemReference.ItemRef = itemInList;
            thisItemReference.CatalogueListing = itemListing;
            thisItemReference.LoadThumbnailSprite();
            thisItemReference.LoadModel();

            itemListing.SetActive(false);
            itemListing.name = $"Item: {thisItemReference.ItemRef.GetItemID()}";
            itemListing.GetComponentInChildren<Text>().text = thisItemReference.ItemRef.GetName();
            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => LoadiDesignScene(thisItemReference, "ARManipulation"));
            itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => LoadiDesignScene(thisItemReference, "itemInformationView"));
            itemListing.transform.SetParent(content.transform, false);

            SetListingSize(itemListing);

            itemManager.ItemReferences.Add(thisItemReference);
            itemManager.itemListings.Add(itemListing);
            itemManager.disabledListings.Add(itemListing);
        }
    }

    //Sets the box size of each listing
    private void SetListingSize(GameObject listing)
    {
        GameObject scrollView = GameObject.Find("Scroll View");

        float width = scrollView.GetComponent<RectTransform>().rect.width - 80;
        float height = 150;

        RectTransform rt = listing.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
    }

    //Finds all categories found in the loaded items.
    //Instantiates category listings/buttons for each category found.
    private void GenerateCategoryListings()
    {
        Debug.Log("Generating Categories");
        foreach (ItemReference item in itemManager.ItemReferences)
        {
            foreach (string category in item.ItemRef.GetCategories())
            {
                if (!FoundCategories.Contains(category))
                {
                    FoundCategories.Add(category);
                }
            }
        }

        FoundCategories.Sort();

        foreach (string category in FoundCategories)
        {
            GameObject CategoryListing = Instantiate(CategoryListingPrefab);

            itemManager.categoryListings.Add(CategoryListing);
            itemManager.visibleListings.Add(CategoryListing);

            CategoryListing.GetComponentInChildren<Text>().text = category;
            CategoryListing.name = $"Category: {category}";
            CategoryListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = GetCategoryThumbnail(category);
            CategoryListing.GetComponent<Button>().onClick.AddListener(() => ChangeContentToCategory(category));
            CategoryListing.transform.SetParent(content.transform, false);
            SetListingSize(CategoryListing);
        }
    }

    //Hides all listings then shows only listings that contains given category
    private void ChangeContentToCategory(string category)
    {
        hideListings();

        foreach (ItemReference item in itemManager.ItemReferences)
        {
            if (item.ItemRef.GetCategories().Contains(category))
            {
                item.CatalogueListing.SetActive(true);
                itemManager.visibleListings.Add(item.CatalogueListing);
            }
        }
        itemManager.ShowingItems = true;
    }

    //Hides all curretly visible listings
    //Puts all currently visible listings in a "ListingsToHide" list
    //After all visible listings are in "ListingsToHide" list
    //Remove them from visibleListings list (Can't do this while iterating through list)
    private void hideListings()
    {
        List<GameObject> ListingsToHide = new List<GameObject>();

        foreach (GameObject item in itemManager.visibleListings)
        {
            item.SetActive(false);
            itemManager.disabledListings.Add(item);
            ListingsToHide.Add(item);
        }

        foreach (GameObject category in itemManager.categoryListings)
        {
            category.SetActive(false);
            itemManager.disabledListings.Add(category);
            ListingsToHide.Add(category);
        }

        foreach (GameObject toHide in ListingsToHide)
        {
            if (itemManager.visibleListings.Contains(toHide))
            {
                itemManager.visibleListings.Remove(toHide);
            }

            else if (itemManager.categoryListings.Contains(toHide))
            {
                itemManager.visibleListings.Remove(toHide);
            }
        }
    }

    //Searches items and returns the thumbnail of the first item found from the given category
    private Sprite GetCategoryThumbnail(string category)
    {
        foreach (ItemReference item in itemManager.ItemReferences)
        {
            if (item.ItemRef.GetCategories().Contains(category))
            {
                return item.GetComponent<ItemReference>().ThumbnailSprite;
            }
        }

        return null;
    }

    //Loads an iDesign scene
    //Updates the focuseditem and loads the scene based on the variables given.
    //The focused item is the item that should be called when referencing the item
    //in another scene.
    private void LoadiDesignScene(ItemReference item, string scene)
    {
        itemManager.FocusedItem = item;

        MoveListingObjects(itemManager.gameObject, false, false);

        itemManager.HoldingListings = true;

        SceneManager.LoadScene(scene);
    }

    //Moves item listings and category listings to the given location object.
    //Boolean fields enable or disable listings from view on launch.
    public void MoveListingObjects(GameObject locationObject, bool showItems, bool showCategories)
    {
        foreach (GameObject itemListing in itemManager.itemListings)
        {
            itemListing.transform.SetParent(locationObject.transform, false);
            itemListing.SetActive(showItems);
        }

        foreach (GameObject categoryListing in itemManager.categoryListings)
        {
            categoryListing.transform.SetParent(locationObject.transform, false);
            categoryListing.SetActive(showCategories);
        }

        if (locationObject == itemManager.gameObject)
        {
            Debug.Log("Saving items into Item Manager Object");
            itemManager.HoldingListings = true;
        }
        else if(locationObject == content)
        {
            Debug.Log("Saving items into Content Object");
            itemManager.HoldingListings = false;
        }
    }

    //When the back button on the catalogue is pressed.
    //If currently showing items, go back to catagory view
    //Else go back to main menu
    public void CategoryBackButton()
    {
        if (itemManager.ShowingItems)
        {
            hideListings();

            foreach (GameObject categoryListing in itemManager.categoryListings)
            {
                categoryListing.SetActive(true);
            }

            itemManager.ShowingItems = false;
        }
        else
        {
            GameObject locationObject = GameObject.Find("Item Manager");
            MoveListingObjects(locationObject, false, false);
            SceneManager.LoadScene("Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
