using System.Collections;

public class Item
{
    private int itemID;
    private string name;
    private float price;
    private string URL;
    private string desc;
    private ArrayList categories;
    private Specs specs;
    public int numberOfClicks = 0;

    public Item(int itemID, string name, float price, string URL, string desc)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        this.URL = URL;
        this.desc = desc;
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

    public ArrayList GetCategories()
    {
        return categories;
    }

    public void AddCategory(string category)
    {
        categories.Add(category);
    }

    public string GetSpecs()
    {
        return specs.GetSpecs();
    }
}