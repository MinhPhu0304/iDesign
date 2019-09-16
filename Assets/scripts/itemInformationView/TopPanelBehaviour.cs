using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopPanelBehaviour : MonoBehaviour
{
    public GameObject TitleItemNameText;
    public GameObject FavouriteButton;
    private User currentUser;
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

        updateFavourtieButton();
        
    }

    private void updateFavourtieButton()
    {
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        
        if (currentUser.GetFavourites().Contains(currentItem))
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

    public void clickFavouite()
    {
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;

        if (!currentUser.GetFavourites().Contains(currentItem))
        {
            favouriteItem();
        }
        else
        {
            unfavouriteItem();
        }

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
}
