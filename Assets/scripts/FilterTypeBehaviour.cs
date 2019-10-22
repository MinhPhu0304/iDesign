using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterTypeBehaviour : MonoBehaviour
{
    public GameObject ParentPanel;
    public GameObject TypePrefab;
    LoadCatalog CatalogueScript;

    // Start is called before the first frame update
    void Start()
    {
        CatalogueScript = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        GenerateCategories(CatalogueScript.foundCategories);
        

    }

    private void GenerateCategories(List<string> categories)
    {
        foreach (string FoundCategory in categories)
        {
            Debug.Log("Found "+FoundCategory);

            GameObject CategoryButton = Instantiate(TypePrefab);

            CategoryButton.GetComponentInChildren<Text>().text = FoundCategory;
            CategoryButton.name = FoundCategory;

            SetButtonSize(CategoryButton);

            CategoryButton.transform.SetParent(ParentPanel.transform, false);
        }
    }

    private void GenerateExtras()
    {

    }

    private void SetButtonSize(GameObject Button)
    {
        float SIDEGAP = 20f;
        float HEIGHT = 150;


        RectTransform rt = Button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(ParentPanel.GetComponent<RectTransform>().rect.width - SIDEGAP, HEIGHT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
