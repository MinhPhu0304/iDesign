using System;
using System.Collections.Generic;

[Serializable]
public class Item : IComparable
{
    private int itemID;
    private string name;
    private float price;
    private string URL;
    private string desc;
    private List<string> categories = new List<string>();
    private string brand;
    private string designer;
    private Specs specs;
    private int numberOfClicks;

    public Item(int itemID, string name, float price, string URL, string desc) : this(itemID,name, price, URL, desc, 0)
    { 
    }

    public Item(int itemID, string name, float price, string URL, string desc, int noClick) : this(itemID, name, price, URL, desc, new Specs(0,0,0, "n/a"), noClick)
    {
    }

    public Item(int itemID, string name, float price, string URL, string desc, Specs specs, int noClick)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        this.URL = URL;
        this.desc = desc;
        this.specs = specs;
        noClick = 0;
    }

    public int GetItemID()
    {
        return itemID;
    }

    public void setNumberOfClick(int numberOfClick)
    {
        this.numberOfClicks = numberOfClick;
    }

    public int getNumberOfClick()
    {
        return numberOfClicks;
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

    public string GetBrand()
    {
        return brand;
    }

    public string GetDesigner()
    {
        return designer;
    }

    public void SetBrand(string brand)
    {
        this.brand = brand;
    }
    public void SetDesigner(string designer)
    {
        this.designer = designer;
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

    public int CompareTo(object obj)
    {
        Item otherItem = (Item)obj;

        int otherItemClicks = otherItem.numberOfClicks;

        return numberOfClicks - otherItemClicks;
    }
}
