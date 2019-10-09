using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    public Text textExtract;
    public Text textChange;
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
        //Allows access of LoadCatalog scipt's methods via its attachment to Viewport canvas
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        string categoryName;
        string brandName;
        string designerName;
        string filter;

        //If text object provided contains text matching 'filter', then program knows which sorting function to use
        filter = textFilter.text;

        if (filter == "Category")
        {
            categoryName = textExtract.text;

            scriptToAccess.categoryGenerate(categoryName);

            textChange.text = categoryName;
        }

        if (filter == "Brand")
        {
            brandName = textExtract.text;

            scriptToAccess.brandGenerate(brandName);

            textChange.text = brandName;
        }

        if (filter == "Designed by")
        {
            designerName = textExtract.text;

            scriptToAccess.designerGenerate(designerName);

            textChange.text = designerName;
        }

        if (filter == "All")
        {
            scriptToAccess.resetListing();
        }

    }
}



