using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    private int itemID;
    private string name;
    private float price;
    private string URL;
    private string desc;
    private List<string> categories = new List<string>();
    private Specs specs;
    public int numberOfClicks = 0;

    public Item(int itemID, string name, float price, string URL, string desc)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        this.URL = URL;
        this.desc = desc;
        this.specs = new Specs(0, 0, 0, "n/a");
    }
    public Item(int itemID, string name, float price, string URL, string desc, Specs specs)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        this.URL = URL;
        this.desc = desc;
        this.specs = specs;
    }

    public int GetItemID()
    {
        return itemID;
    }

    public string GetName()
    {
        return name;
    }

    public float GetPrice()
    {
        return price;
    }
    public string GetURL()
    {
        return URL;
    }

    public string GetDesc()
    {
        return desc;
    }

    public List<string> GetCategories()
    {
        return categories;
    }

    public void AddCategory(string category)
    {
        categories.Add(category);
    }

    public void AddCategory(string[] categoryArray)
    {
        foreach (string category in categoryArray)
        {
            categories.Add(category);
        }
    }

    public string GetSpecs()
    {
        return specs.GetSpecs();
    }

    override
    public string ToString()
    {
        return name;
    }

    override
    public Boolean Equals(Object obj)
    {
        Item compareItem = (Item) obj;
        return (itemID == compareItem.itemID);
    }
}
