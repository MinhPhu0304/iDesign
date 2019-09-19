using System.Collections.Generic;
using UnityEngine;

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

    public string formatFavourites()
    {
        string formatedFavourites = "";

        for(int i =0; i < favourites.Count - 1; ++i)
        {
            formatedFavourites += favourites[i].GetName()+",";
        }

        if (favourites.Count == 0)
        {
            formatedFavourites = "";
        }
        else if (favourites.Count == 1)
        {
            formatedFavourites = favourites[0].GetName();
        } 
        else
        {
            formatedFavourites += favourites[favourites.Count - 1];
        }

        Debug.Log(formatedFavourites);
        return formatedFavourites;
    }
}