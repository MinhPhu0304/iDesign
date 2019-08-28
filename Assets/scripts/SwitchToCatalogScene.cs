using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToCatalogScene : MonoBehaviour
{
    public void SwitchToCatalog()
    {
        SceneManager.LoadScene("Catalog");
    }
}
