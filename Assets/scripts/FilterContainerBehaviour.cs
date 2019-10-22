using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterContainerBehaviour : MonoBehaviour
{
    public GameObject CategoryButton;
    public GameObject DesignerButton;
    public GameObject BrandButton;

    public GameObject CategoryPanel;
    public GameObject DesignerPanel;
    public GameObject BrandPanel;

    //Opens panel based on button pressed.
    public void ButtonPressed(GameObject Pressed)
    {
        if (Pressed == CategoryButton)
        {
            CategoryPanel.SetActive(true);
        }
        else if (Pressed == DesignerButton)
        {
            DesignerPanel.SetActive(true);
        }
        else if (Pressed == BrandButton)
        {
            BrandPanel.SetActive(true);
        }
    }

    //If a panel is open hide the open panel
    //Otherwise close the filter view
    public void BackPressed()
    {
        if (IsPanelsActivated())
        {
            CategoryPanel.SetActive(false);
            DesignerPanel.SetActive(false);
            BrandPanel.SetActive(false);
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }

    }

    //Checks if any of the type panels are visible
    public bool IsPanelsActivated()
    {
        return CategoryPanel.activeSelf || DesignerPanel.activeSelf || BrandPanel.activeSelf;
    }
}
