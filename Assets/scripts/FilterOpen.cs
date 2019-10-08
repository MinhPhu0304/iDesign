using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    public Text text;

    public void OpenPanel()
    {
        if (panelToOpen != null)
        {
            bool isActive = panelToOpen.activeSelf;

            panelToOpen.SetActive(!isActive);
        }
    }

    public void showAny()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.resetListing();
    }
    //Displays items under Office
    public void showOffice()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Office");

        text.text = "Office";
    }
    //Displays items under Living Room category
    public void showLiving()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Living Room");

        text.text = "Living Room";
    }
    //Displays items made and designed by Google
    public void showGoogle()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Google");

        text.text = "Google";
    }
    //Displays items made and designed by Ikea
    public void showIkea()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Ikea");

        text.text = "Ikea";
    }
    //Displays items made for Harvey Norman and designed by Parkland
    public void showHarveyNorm()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Harvey Norman");

        text.text = "Harvey Norman";
    }
    //Displays items made for The Warehouse and designed by Living&Co
    public void showWarehouse()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("The Warehouse");

        text.text = "The Warehouse";
    }
    //Displays items designed by Parkland
    public void showParkland()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.designerGenerate("Parkland");

        text.text = "Parkland";
    }
}
