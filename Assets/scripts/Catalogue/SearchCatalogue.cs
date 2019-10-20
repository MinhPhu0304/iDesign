using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchCatalogue : MonoBehaviour
{
   
    private ItemManager itemManager;

    public GameObject SearchText;

    // Start is called before the first frame update
    private void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();
    }

    public void GetSearchResults()
    {
        if (SearchText.GetComponent<Text>().text.Length == 0)
        {
            return;
        }

        List<GameObject> SearchResults = new List<GameObject>();

        foreach (ItemReference item in itemManager.ItemReferences)
        {
            if (item.ItemRef.GetName().IndexOf(SearchText.GetComponent<Text>().text, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                SearchResults.Add(item.CatalogueListing);
            }
            else
            {
                itemManager.disabledListings.Add(item.CatalogueListing);
            }
        }

        itemManager.visibleListings = SearchResults;

        RefreshVisibleListings();
    }

    public void RefreshVisibleListings()
    {
        foreach (GameObject listing in itemManager.disabledListings)
        {
            listing.SetActive(false);
        }

        foreach (GameObject listing in itemManager.visibleListings)
        {
            listing.SetActive(true);
        }
    }


}
