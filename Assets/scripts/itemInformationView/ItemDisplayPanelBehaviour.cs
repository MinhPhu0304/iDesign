using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayPanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static Item currentItem;
    public GameObject ItemNameText;
    public GameObject itemPrice;
    public GameObject DescAndSpecsContent;
    void Start()
    {
        string desc = "Front legs 2”x2” tapered, back legs 1 ¼” thick (curved), top rail and seat rail 1” thick, under framing 1 ¼” x7 / 8” slates 1”x3 / 8” sheet frame and shape of wood 1 ¾” thick seat slates 1 ½”x1 ½”x3 / 4 back height 34” seat height 18”, depth 18”, seat front 18”, width of back 16”. ";
        currentItem = new Item(5, "Chair", 10.0f, "https://www.trademe.co.nz/", desc, (new Specs(5.0f, 15.0f, 5.0f, "Brown")));
        Text name = ItemNameText.GetComponent<Text>();
        name.text = currentItem.GetName();

        itemPrice.GetComponent<Text>().text = string.Format("{0:C}", currentItem.GetPrice());
        currentItem.GetPrice();


        Text descAndSpecs = DescAndSpecsContent.GetComponentInChildren<Text>();
        descAndSpecs.text = "Description: \n\n" + currentItem.GetDesc() + "\n\nSpecifications: \n\n" + currentItem.GetSpecs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
