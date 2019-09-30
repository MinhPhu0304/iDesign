using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterOpen : MonoBehaviour
{
    public GameObject filterPanel;

    public void OpenPanel()
    {
        if(filterPanel != null)
        {
            bool isActive = filterPanel.activeSelf;

            filterPanel.SetActive(!isActive);
        }
    }
}
