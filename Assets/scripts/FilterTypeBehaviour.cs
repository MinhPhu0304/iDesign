using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterTypeBehaviour : MonoBehaviour
{
    public GameObject ParentPanel;
    public GameObject TypePrefab;
    LoadCatalog CatalogueScript;
    public GameObject ContentForScroll;

    // Start is called before the first frame update
    void Start()
    {
        CatalogueScript = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        if (ParentPanel == GameObject.Find("Category Container"))
        {
            GenerateButtons(CatalogueScript.foundCategories);
        }
        else if (ParentPanel == GameObject.Find("Designer Container"))
        {
            GenerateButtons(CatalogueScript.foundDesigners);
        }
        else if (ParentPanel == GameObject.Find("Brand Container"))
        {
            GenerateButtons(CatalogueScript.foundBrands);
        }
        else {
            Debug.Log("Couldn't find parent");
        }

    }

    //Generated buttons dynamically depending on number of strings in foundStrings.
    private void GenerateButtons(List<string> foundStrings)
    {
        foreach (string FoundCategory in foundStrings)
        {
            GameObject FilterButton = Instantiate(TypePrefab);

            FilterButton.GetComponentInChildren<Text>().text = FoundCategory;
            FilterButton.name = FoundCategory;

            FilterButton.GetComponent<Button>().onClick.AddListener(() => ActivateFilter(FilterButton));

            SetButtonSize(FilterButton);
            if (ContentForScroll != null)
            {
                FilterButton.transform.SetParent(ContentForScroll.transform, false);
            }
            else
            {
                FilterButton.transform.SetParent(ParentPanel.transform, false);
            }
            
        }
    }

    //Sets the search text to the button that was pressed.
    private void ActivateFilter(GameObject ButtonPressed)
    {
        string ButtonText = ButtonPressed.GetComponentInChildren<Text>().text;

        GameObject SearchBox = GameObject.Find("InputField");

        SearchBox.GetComponent<InputField>().text = ButtonText;
        CatalogueScript.showingItems = true;
        CatalogueScript.SearchCatalog();
    }

    //Sets size of the button.
    private void SetButtonSize(GameObject Button)
    {
        float SIDEGAP = 20f;
        float HEIGHT = 150;

        RectTransform rt = Button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(ParentPanel.GetComponent<RectTransform>().rect.width - SIDEGAP, HEIGHT);
    }
}
