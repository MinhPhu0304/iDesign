using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public GameObject ObjectToPlace;

    public ItemReference FocusedItem;

    public List<Item> loadedItems;

    public bool HoldingListings = false;
    public bool ShowingItems = false;

    public List<ItemReference> ItemReferences;
    public List<GameObject> itemListings; // Holds all listings loaded from database
    public List<GameObject> categoryListings; //Holds all category listings found from items
    public List<GameObject> visibleListings; // Holds listings CURRENTLY VISIBLE in the caralogue
    public List<GameObject> disabledListings; // Holds listings that are CURRENTLY DISABLED

    //public static ItemManager Instance;
    
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //TODO: Take a GameObjectListing and return the Item equivalent ItemObject.
    public Item FindItemByID(GameObject go)
    {
        Item foundItem = null;

        return foundItem;
    }
    //https://www.youtube.com/watch?v=5p2JlI7PV1w
    //https://www.youtube.com/watch?v=AvuuX4qxC_0
}
