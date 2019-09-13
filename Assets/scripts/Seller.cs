using System.Collections.Generic;

public class Seller : User
{
    private List<Item> sellerItems;
    public Seller(int userID, string username) : base(userID, username)
    {
        sellerItems = new List<Item>();
    }

    public List<Item> GetSellerItem()
    {
        return sellerItems;
    }

    public void uploadItem()
    {
        sellerItems = new List<Item>();
    }
}