using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PopularItemsBehaviour : MonoBehaviour
{
    public List<Item> itemList;
    public GameObject popularItemPanel;
    //Setting the number of popular items to display to 10
    public int numberOfPopularItems = 10;
    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Item>();
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

        //Checking whether there is less items than the set number of popular items
        //IF there are then set the number of popular item to display to number of the list
        if(itemList.Count < numberOfPopularItems)
        {
            numberOfPopularItems = itemList.Count;
        }

        GameObject content = GameObject.Find("Content");

        }


    }
}
