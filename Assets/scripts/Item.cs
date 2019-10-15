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
    private List<string> brands = new List<string>();
    private List<string> designers = new List<string>();
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

    public List<string> GetBrand()
    {
        return brands;
    }

    public void AddBrand(string brand)
    {
        brands.Add(brand);
    }

    public void AddBrand(string[] brandArray)
    {
        foreach (string brand in brandArray)
        {
            brands.Add(brand);
        }
    }

    public List<string> GetDesigner()
    {
        return designers;
    }

    public void AddDesigner(string designer)
    {
        designers.Add(designer);
    }

    public void AddDesigner(string[] designerArray)
    {
        foreach (string designer in designerArray)
        {
            designers.Add(designer);
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

    public int CompareTo(object obj)
    {
        Item otherItem = (Item)obj;

        int otherItemClicks = otherItem.numberOfClicks;

        if(numberOfClicks > otherItemClicks)
        {
            return 1;
        }
        else if(numberOfClicks < otherItemClicks)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
