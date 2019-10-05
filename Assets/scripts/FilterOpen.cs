using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;

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

        scriptToAccess.categoryGenerate("Office"); ;
    }
    //Displays items under Living Room category
    public void showLiving()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Living Room");
    }
    //Displays items made and designed by Google
    public void showGoogle()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Google");
    }
    //Displays items made and designed by Ikea
    public void showIkea()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Ikea");
    }
    //Displays items made for Harvey Norman and designed by Parkland
    public void showHarveyNorm()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Harvey Norman");
    }
    //Displays items made for The Warehouse and designed by Living&Co
    public void showWarehouse()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("The Warehouse");
    }

}
