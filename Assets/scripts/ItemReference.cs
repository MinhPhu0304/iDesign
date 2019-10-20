using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Links items to models, thumbnails and listings
public class ItemReference : MonoBehaviour
{
    public Item ItemRef;
    public Sprite ThumbnailSprite;
    public GameObject Model;
    public GameObject CatalogueListing;

    /*public ItemReference(Item item)
    {
        ItemRef = item;
        LoadThumbnailSprite();
        LoadModel();
    }*/

    public void LoadThumbnailSprite()
    {
        ThumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ItemRef.GetItemID()}") as Sprite;

        if (CatalogueListing != null)
        {
            CatalogueListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = ThumbnailSprite;
        }
    }

    public void LoadModel()
    {
        Model = Resources.Load($"Models/{ItemRef.GetItemID()}") as GameObject;
    }

}
