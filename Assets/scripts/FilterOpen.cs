using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;

    public void OpenPanel()
    {
        //scriptToAccess.hideListings();
        if (panelToOpen != null)
        {
            bool isActive = panelToOpen.activeSelf;

            panelToOpen.SetActive(!isActive);
        }
    }

    public void showOffice()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryOffice();
    }

    public void showLiving()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryLiving();
    }

    public void showGoogle()
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        scriptToAccess.categoryGoogle();
    }

}
