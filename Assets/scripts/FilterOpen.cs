using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    public Text textExtract;
    public Text textChange;

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

    public void showCategories(string categoryName)
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        categoryName = textExtract.text;

        scriptToAccess.categoryGenerate(categoryName);

        textChange.text = categoryName;
    }

    public void showBrands(string brandName)
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        brandName = textExtract.text;

        scriptToAccess.brandGenerate(brandName);

        textChange.text = brandName;
    }

    public void showDesigners(string designerName)
    {
        LoadCatalog scriptToAccess = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        designerName = textExtract.text;

        scriptToAccess.designerGenerate(designerName);

        textChange.text = designerName;
    }
}



