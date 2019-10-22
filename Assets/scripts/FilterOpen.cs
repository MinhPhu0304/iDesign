﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject filterButtonPrefab;
    public Text textExtract;
    public Text textFilter;

    //Allows functionality of opening a panel
    public void OpenPanel()
    {
        if (panelToOpen != null)
        {
            bool isActive = panelToOpen.activeSelf;

            panelToOpen.SetActive(!isActive);
        }
    }
    public void filter()
    {
        StartCoroutine("FilterPress");
    }
    private IEnumerator FilterPress()
    {
        //Allows access of LoadCatalog.cs script's methods via its attachment to Viewport canvas
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        string categoryName;
        string brandName;
        string designerName;
        string filterTo;

        

        //Text object provided contains text that is loaded into 'filter', then program knows which sorting function to use
        filterTo = textFilter.text;

        if (filterTo == "Category")
        {
            categoryName = textExtract.text;

            scriptToAccess.ChangeContentToCategory(categoryName);
        }

        if (filterTo == "Brand")
        {
            brandName = textExtract.text;

            scriptToAccess.brandGenerate(brandName);
        }

        if (filterTo == "Designed by")
        {
            designerName = textExtract.text;

            scriptToAccess.designerGenerate(designerName);
        }

        if (filterTo == "Any")
        {
            scriptToAccess.resetListing();
        }
        yield return null;
    }
}



