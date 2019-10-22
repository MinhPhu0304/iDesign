using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemDisplayPanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static Item currentItem;
    public GameObject ItemNameText;
    public GameObject itemPrice;
    public GameObject DescAndSpecsContent;
    public GameObject image;
    private ItemManager itemManager;

    void Start()
    {
        //If not a complete gameobject don't continue
        if (ItemNameText is null)
        {
            return;
        }

        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();
        
        Text name = ItemNameText.GetComponent<Text>();
        name.text = currentItem.GetName();

        itemPrice.GetComponent<Text>().text = "$" + string.Format("{0:N}", currentItem.GetPrice());
        currentItem.GetPrice();

        Sprite imageSprite = Resources.Load<Sprite>($"Thumbnails/{currentItem.GetName()}") as Sprite;
        image.GetComponent<Image>().sprite = imageSprite;

        Text descAndSpecs = DescAndSpecsContent.GetComponentInChildren<Text>();
        descAndSpecs.text = "Description: \n\n" + currentItem.GetDesc() + "\n\nSpecifications: \n\n" + currentItem.GetSpecs();
    }

    public void SetCurrentItem(Item item)
    {
        currentItem = item;
        SceneManager.LoadScene("itemInformationView");        
    }

    public void NavigateARScene()
    {
        GameObject selectedObject = Resources.Load($"Models/{currentItem.GetName()}") as GameObject;

        itemManager.ObjectToPlace = selectedObject;
        SceneManager.LoadScene("ARManipulation");
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
