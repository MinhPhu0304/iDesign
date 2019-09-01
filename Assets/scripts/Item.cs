public class Item
{
	private string name;
	private float price;
	private string URL;
    private string desc;
    private string specs;
	public Item(string name, float price, string URL, string desc, string specs)
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
        return specs;
    }
}