using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelBehaviour : MonoBehaviour
{
    private ItemManager itemManager;
    public GameObject ItemImagePrefab;
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();
        GenerateItemButtons();
    }

    //Generates itemPanel buttons dynamically depending on object
    public void GenerateItemButtons()
    {
        foreach (GameObject model in itemManager.selectableModels)
        {
            GameObject modelButton = Instantiate(ItemImagePrefab);

            modelButton.transform.SetParent(Panel.transform, false);

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{model.name}") as Sprite;
            modelButton.GetComponent<Image>().sprite = thumbnailSprite;

            modelButton.GetComponent<Button>().onClick.AddListener(() => ChangePlacedItem(model));
        }
    }

    //Changes the item to the item that is pressed.
    public void ChangePlacedItem(GameObject model)
    {
        itemManager.ObjectToPlace = model;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
