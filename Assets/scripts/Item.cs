public class Item
{
	private string name;
	private float price;
	private string URL;
    private string desc;
    private Specs specs;
    public int numberOfClicks = 0;
	public Item(string name, float price, string URL, string desc, string desc1, Specs specs)
	{
		this.name = name;
		this.price = price;
		this.URL = URL;
        this.desc = desc;
        this.specs = specs;
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

    public string GetSpecs()
    {
        return specs.GetSpecs();
    }
}