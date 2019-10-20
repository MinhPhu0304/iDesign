using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemDisplayPanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Item currentItem;
    public GameObject ItemNameText;
    public GameObject itemPrice;
    public GameObject DescAndSpecsContent;
    public GameObject image;

    private ItemManager itemManager;

    void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();

        //If not a complete gameobject don't continue
        if (itemManager.FocusedItem is null)
        {
            return;
        }

        currentItem = itemManager.FocusedItem.ItemRef;

        Text name = ItemNameText.GetComponent<Text>();
        name.text = currentItem.GetName();

        itemPrice.GetComponent<Text>().text = string.Format("{0:C}", currentItem.GetPrice());
        currentItem.GetPrice();

        Sprite imageSprite = itemManager.FocusedItem.ThumbnailSprite;
        image.GetComponent<Image>().sprite = imageSprite;

        Text descAndSpecs = DescAndSpecsContent.GetComponentInChildren<Text>();
        descAndSpecs.text = "Description: \n\n" + currentItem.GetDesc() + "\n\nSpecifications: \n\n" + currentItem.GetSpecs();
    }

    public void SetCurrentItem(Item item)
    {
        /*currentItem = item;
        SceneManager.LoadScene("itemInformationView"); */

        //currentItem = itemManager.PlacedItem;
        SceneManager.LoadScene("itemInformationView");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
