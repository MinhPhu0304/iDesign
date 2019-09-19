using System;
using System.Collections.Generic;
using System;

[Serializable]
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

    //Add an Item to the user's favourites
    public void addFavourite(Item favouritedItem)
    {
        favourites.Add(favouritedItem);
        Console.WriteLine(favourites.Count);
    }

    //Returns a formatted versions of the favourites
    public string formatFavourites()
    {
        return string.Join(",", favourites);
    }

    public void removeItem(Item item)
    {
        favourites.Remove(item);
    }

    override
    public string ToString()
    {
        return username;
    }
}