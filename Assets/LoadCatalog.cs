using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class LoadCatalog : MonoBehaviour
{
    Resolution[] resolutions;

    [SerializeField] GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Testing
        GameObject content = GameObject.Find("Content");
        resolutions = Screen.resolutions;
        for( int i = 0; i < 10; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            //button.GetComponent<Text>().text = i.ToString();
            button.GetComponent<Button>().onClick.AddListener(Testmethod);
            button.transform.SetParent(content.transform);
        }
    }

    void Testmethod()
    {
        Debug.Log("Button Pressed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
