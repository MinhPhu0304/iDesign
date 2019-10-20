using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Generates items and stores them in the Item Manager singleton class.
//This will be replaced with the script that loads items from the database.
public class GenerateItems : MonoBehaviour
{
    ItemManager itemManager;

    void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        if (itemManager.itemListings.Count == 0)
        {
            Debug.Log("GenerateItems: Generating Items");
            LoadItems();
            CreateItemReferences();
        }
        
    }

    public void LoadItems()
    {
        Item gotItem;

        itemManager.loadedItems.Add(new Item(0, "Chair", 10.00f, "http://www.google.com", "No description set."));
        gotItem = (Item)itemManager.loadedItems[0];
        gotItem.AddCategory(new string[] { "Office", "Chairs", "Desks" });
        gotItem.AddBrand(new string[] { "Ikea" });
        gotItem.AddDesigner(new string[] { "Ikea" });

        itemManager.loadedItems.Add(new Item(1, "Couch", 100.00f, "http://www.google.com", "No description set."));
        gotItem = (Item)itemManager.loadedItems[1];
        gotItem.AddCategory(new string[] { "Living Room", "Couches", "Lounge" });
        gotItem.AddBrand(new string[] { "Harvey Norman" });
        gotItem.AddDesigner(new string[] { "Parkland" });

        itemManager.loadedItems.Add(new Item(2, "Table", 20.00f, "http://www.google.com", "No description set."));
        gotItem = (Item)itemManager.loadedItems[2];
        gotItem.AddCategory(new string[] { "Living Room", "Tables", "Dining Room", "Office" });
        gotItem.AddBrand(new string[] { "The Warehouse" });
        gotItem.AddDesigner(new string[] { "Living & Co" });

        itemManager.loadedItems.Add(new Item(3, "Andy", 0.00f, "http://www.google.com", "Andy the android."));
        gotItem = (Item)itemManager.loadedItems[3];
        gotItem.AddCategory(new string[] { "Google", "Android" });
        gotItem.AddDesigner(new string[] { "Google" });
    }

    public void CreateItemReferences()
    {
        /*foreach (Item item in itemManager.loadedItems)
        {
            GameObject content = GameObject.Find("Content");
            GameObject itemReference = new GameObject();
            itemReference.transform.SetParent(content.transform, false);
            itemManager.ItemReferences.Add(itemReference);
            Debug.Log("Added " + itemReference + " to item references");
        }*/
    }
}
