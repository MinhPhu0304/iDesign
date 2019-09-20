using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TopPanelBehaviour : MonoBehaviour
{
    public GameObject TitleItemNameText;
    public GameObject FavouriteButton;
    private static User currentUser;
    // Start is called before the first frame update
    private Sprite selectedSprite;
    private Sprite defaultSprite;
    private string favouriteBuffer;
    void Start()
    {
        defaultSprite = Resources.Load("favourite", typeof(Sprite)) as Sprite;
        selectedSprite = Resources.Load("favouriteSelected", typeof(Sprite)) as Sprite;
        currentUser = new User(1, "guy01");

        Text name = TitleItemNameText.GetComponent<Text>();
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        if (currentItem != null)
            name.text = ItemDisplayPanelBehaviour.currentItem.GetName();

        //Checking if the text file with favourites exist. If it does we can then read in
        if (File.Exists(Application.persistentDataPath + "/favourites.txt"))
        {
            string readInFile = loadFIle();

            //Save in to the buffer which will hold the previously stored favourites
            favouriteBuffer = readInFile;
            if (readInFile.Contains(currentItem.GetName()))
            {
                //Remove the text with the currentItem so if it is unfavourited it will be removed from the favourite text file
                favouriteBuffer = favouriteBuffer.Replace(currentItem.GetName(), "");
                //Re favourite the item as it would of been destroyed when the scene changed
                favouriteItem();
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
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;

        //Checking the item is already favourited or  not
        if (!isFavourite())
        {
            favouriteItem();
        }
        else
        {
            unfavouriteItem();
        }

        //Save the favourites file and update the UI
        saveFavouriteFile();
        updateFavourtieButton();
    }

    private void unfavouriteItem()
    {
        currentUser.GetFavourites().Remove(ItemDisplayPanelBehaviour.currentItem);
    }

    private void favouriteItem()
    {
        currentUser.addFavourite(ItemDisplayPanelBehaviour.currentItem);
    }

    //This method saves the favourite files into a text file
    public void saveFavouriteFile()
    {
        string path = Application.persistentDataPath + "/favourites.txt";

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(favouriteBuffer + currentUser.formatFavourites());
        writer.Close();
    }

    //This method check if the currentItem is already favourited
    public Boolean isFavourite()
    {
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        Debug.Log(currentUser.formatFavourites());
        return (currentUser.formatFavourites().Contains(currentItem.GetName()));
    }

    //Reads the input from the favourites text file and return the contents as a string
    public string loadFIle()
    {
        string path = Application.persistentDataPath + "/favourites.txt";

        StreamReader reader = new StreamReader(path);
        string inputFaves = reader.ReadToEnd();
        reader.Close();

        return inputFaves;
    }
}
