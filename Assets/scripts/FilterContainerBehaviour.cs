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

    public void BackPressed()
    {
        CategoryPanel.SetActive(false);
        DesignerPanel.SetActive(false);
        BrandPanel.SetActive(false);
    }
}
