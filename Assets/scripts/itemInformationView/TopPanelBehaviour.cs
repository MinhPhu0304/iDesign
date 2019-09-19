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
    void Start()
    {
        defaultSprite = Resources.Load("favourite", typeof(Sprite)) as Sprite;
        selectedSprite = Resources.Load("favouriteSelected", typeof(Sprite)) as Sprite;
        currentUser = new User(1, "guy01");

        Text name = TitleItemNameText.GetComponent<Text>();
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        if (currentItem != null)
            name.text = ItemDisplayPanelBehaviour.currentItem.GetName();

        string readInFile = loadFIle();

        if (readInFile.Contains(currentItem.GetName()))
        {
            favouriteItem();
        }

        Debug.Log(readInFile);

        updateFavourtieButton();

    }

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

    // Update is called once per frame
    void Update()
    {

    }

    public void GoBack()
    { 
        SceneManager.LoadScene("Catalog");
    }

    public void clickFavourite()
    {

        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        string readInFile = loadFIle();

        if (readInFile.Contains(currentItem.GetName()))
        {
            unfavouriteItem();
        }
        else if (!isFavourite())
        {
            Debug.Log("Favouriting item");
            favouriteItem();
        }
        else
        {
            Debug.Log("unfavouriting item");
            unfavouriteItem();
        }

        saveFile();
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

    public void saveFile()
    {
        string path = Application.persistentDataPath + "/favourites.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(currentUser.formatFavourites());
        writer.Close();
    }

    public Boolean isFavourite()
    {
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        Debug.Log(currentUser.formatFavourites());
        return (currentUser.formatFavourites().Contains(currentItem.GetName()));
    }

    public string loadFIle()
    {
        string path = Application.persistentDataPath + "/favourites.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string inputFaves = reader.ReadToEnd();
        reader.Close();

        return inputFaves;
    }
}
