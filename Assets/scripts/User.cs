using System.Collections.Generic;

public class User
{
    private int userID;
    private string username;
    private List<Item> favourites;

    public User(int userID, string username)
    {
        this.userID = userID;
        this.username = username;
        favourites = new List<Item>();
    }

    public int GetUserID()
    {
        return userID;
    }

    public string GetUsername()
    {
        return username;
    }

    public List<Item> GetFavourites()
    {
        return favourites;
    }

    public void addFavourite(Item favouritedItem)
    {
        favourites.Add(favouritedItem);
    }
}