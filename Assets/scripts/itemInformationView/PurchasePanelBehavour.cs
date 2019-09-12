using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelScript : MonoBehaviour
{
    public GameObject purchasePanel;
    private Item currentItem;
    public GameObject ItemName;
    public GameObject ItemPrice;
    public GameObject PreviewButton;
    public GameObject PurchaseButton;
    // Start is called before the first frame update
    private void Start()
    {
        currentItem = ItemDisplayPanel.currentItem;
        HidePanel();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void ShowPanel()
    {
        PreviewButton.GetComponent<Button>().interactable = false;
        PurchaseButton.GetComponent<Button>().interactable = false;

        purchasePanel.gameObject.SetActive(true);

        Text name = ItemName.GetComponent<Text>();
        name.text = currentItem.GetName();

        Text price = ItemPrice.GetComponent<Text>();
        price.text = "Price: $" + currentItem.GetPrice();
    }

    public void HidePanel()
    {
        PreviewButton.GetComponent<Button>().interactable = true;
        PurchaseButton.GetComponent<Button>().interactable = true;

        purchasePanel.gameObject.SetActive(false);
    }

    public void GotoSupplierWebsite()
    {
        ++currentItem.numberOfClicks;
        Application.OpenURL(currentItem.GetURL());
    }
}
