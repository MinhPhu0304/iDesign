using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToCameraScene : MonoBehaviour
{
    public void SwitchToCamera()
    {
        SceneManager.LoadScene("ARManipulation");
    }
}
