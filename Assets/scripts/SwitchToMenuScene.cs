using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchToMenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToMenu()
    { 
        SceneManager.LoadScene("Menu");
    }

    public void SwitchToMenuFronCatalog()
    {
        LoadCatalog CatalogueScript = GameObject.Find("Viewport").GetComponent<LoadCatalog>();

        if (CatalogueScript.showingItems)
        {
            SceneManager.LoadScene("Catalog");
        }
        else
        {
            SwitchToMenu();
        }
    }
}
