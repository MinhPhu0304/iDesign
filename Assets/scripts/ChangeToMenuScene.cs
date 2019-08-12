using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene("Menu");
    }
}