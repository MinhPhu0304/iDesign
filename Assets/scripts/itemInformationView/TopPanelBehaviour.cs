using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class TopPanelBehaviour : MonoBehaviour
{
    public GameObject TitleItemNameText;
    public GameObject FavouriteButton;
    private static User currentUser;
    // Start is called before the first frame update
    private Sprite selectedSprite;
    private Sprite defaultSprite;
    private string favouriteBuffer;
    private Item currentItem;
    string path;
    FileStream stream;
    void Start()
    {
        path = Application.persistentDataPath + "/favourites.txt";

        defaultSprite = Resources.Load("favourite", typeof(Sprite)) as Sprite;
        selectedSprite = Resources.Load("favouriteSelected", typeof(Sprite)) as Sprite;
        currentUser = new User(1, "guy01");

        Text name = TitleItemNameText.GetComponent<Text>();
        currentItem = ItemDisplayPanelBehaviour.currentItem;
        if (currentItem != null)
            name.text = ItemDisplayPanelBehaviour.currentItem.GetName();

        if (File.Exists(path))
        {
            try
            {
                loadFIle();
            } catch(SerializationException ex)
            {
                Debug.Log("File cannot be unserialized!.");
            }
            
        }
        updateFavourtieButton();

    }


    //Update the UI of the favourite button
    private void updateFavourtieButton()
    {
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;

        if (isFavourite())
        {
            FavouriteButton.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            FavouriteButton.GetComponent<Image>().sprite = defaultSprite;
        }
    }


    public void GoBack()
    { 
        SceneManager.LoadScene("Catalog");
    }

    //Controls the behaviour, when clicking the button.
    public void clickFavourite()
    {
        //Checking the item is already favourited or  not
        if (!isFavourite())
        {
            favouriteItem();
        }
        else
        {
            unfavouriteItem();
            Debug.Log("After unfavourite: " + currentUser.formatFavourites());
        }

        //Save the favourites file and update the UI
        saveFavouriteFile();
        updateFavourtieButton();
    }

    private void unfavouriteItem()
    {
        currentUser.GetFavourites().Remove(currentItem);
    }

    private void favouriteItem()
    {
        currentUser.addFavourite(currentItem);
    }

    //This method saves the favourite files into a text file
    public void saveFavouriteFile()
    {
        stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        IFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, currentUser);
        stream.Close();
    }

    //This method check if the currentItem is already favourited
    public Boolean isFavourite()
    {
        return (currentUser.formatFavourites().Contains(currentItem.GetName()));
    }

    //Reads the input from the favourites text file and return the contents as a string
    public void loadFIle()
    {
        stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
        IFormatter formatter = new BinaryFormatter();
        currentUser = (User)formatter.Deserialize(stream);
        stream.Close();
    }

}
