using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopPanelBehaviour : MonoBehaviour
{
    public GameObject TitleItemNameText;
    public GameObject FavouriteButton;
    // Start is called before the first frame update
    void Start()
    {
        Text name = TitleItemNameText.GetComponent<Text>();
        Item currentItem = ItemDisplayPanelBehaviour.currentItem;
        if (currentItem != null)
            name.text = ItemDisplayPanelBehaviour.currentItem.GetName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Catalog");
    }

    public void favouriteItem()
    {
        //favourite item using the User Method
    }
}
