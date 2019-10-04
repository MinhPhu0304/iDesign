using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    Item listing;

    public void OpenPanel()
    {
        //scriptToAccess.hideListings();
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

    public void showOffice()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Office"); ;
    }

    public void showLiving()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Living Room");
    }

    public void showGoogle()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGenerate("Google");
    }

    public void showIkea()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Ikea");
    }

    public void showHarveyNorm()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Harvey Norman");
    }

    public void showLivingCo()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.brandGenerate("Living & Co");
    }

}
