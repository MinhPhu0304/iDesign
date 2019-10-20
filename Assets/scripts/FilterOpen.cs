using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterOpen : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject FilterButtonPrefab;
    public GameObject BrandPanel;
    public GameObject DesignerPanel;
    private ItemManager itemManager;
    public Text textExtract;
    public Text textFilter;

    private void Start()
    {
        itemManager = GameObject.Find("Item Manager").GetComponent<ItemManager>();
        GenerateBrandButtons();
        GenerateDesignerButtons();
    }

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

    private void GenerateBrandButtons()
    {
        Debug.Log("Generating brand buttons");
        List<string> FoundBrands = new List<string>();

        foreach (Item item in itemManager.loadedItems)
        {
            if (!FoundBrands.Contains(item.GetBrand()))
            {
                FoundBrands.Add(item.GetBrand());

                GameObject filterButton = Instantiate(FilterButtonPrefab);

                filterButton.transform.SetParent(BrandPanel.transform, false);
                SetFilterButtonSize(filterButton);
            }
        }
    }

    private void GenerateDesignerButtons()
    {
        List<string> FoundDesigners = new List<string>();

        foreach (Item item in itemManager.loadedItems)
        {
            if (!FoundDesigners.Contains(item.GetDesigner()))
            {
                FoundDesigners.Add(item.GetDesigner());

                GameObject filterButton = Instantiate(FilterButtonPrefab);

                filterButton.transform.SetParent(DesignerPanel.transform, false);
                SetFilterButtonSize(filterButton);
            }
        }
    }

    private void SetFilterButtonSize(GameObject button)
    {
        float width = panelToOpen.GetComponent<RectTransform>().rect.width - 10;
        float height = 150;

        RectTransform rt = button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(width, height);
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

            scriptToAccess.categoryGenerate(categoryName);
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



