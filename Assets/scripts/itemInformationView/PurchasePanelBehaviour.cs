﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelBehaviour : MonoBehaviour
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
        currentItem = ItemDisplayPanelBehaviour.currentItem;
        HidePanel();
    }

    public void ShowPanel()
    {
        PreviewButton.GetComponent<Button>().interactable = false;
        PurchaseButton.GetComponent<Button>().interactable = false;

        purchasePanel.gameObject.SetActive(true);

        Text name = ItemName.GetComponent<Text>();
        name.text = currentItem.GetName();

        Text price = ItemPrice.GetComponent<Text>();
        price.text = "Price: $" + string.Format("{0:N}", currentItem.GetPrice());
    }

    public void HidePanel()
    {
        PreviewButton.GetComponent<Button>().interactable = true;
        PurchaseButton.GetComponent<Button>().interactable = true;

        purchasePanel.gameObject.SetActive(false);
    }

    public void GotoSupplierWebsite()
    { 
        currentItem.setNumberOfClick((currentItem.getNumberOfClick() + 1));
        ItemManager itemMananger = GameObject.Find("Item Manager").GetComponent<ItemManager>();
        itemMananger.updateNumberOfClicks(currentItem);
        Application.OpenURL(currentItem.GetURL());
    }
}
