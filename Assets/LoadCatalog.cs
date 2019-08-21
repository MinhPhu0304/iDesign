using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class LoadCatalog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject content = GameObject.Find("Content");
        GameObject button = new GameObject();

        button.AddComponent<CanvasRenderer>();
        button.AddComponent<RectTransform>();
        Button mButton = button.AddComponent<Button>();
        Image mImage = button.AddComponent<Image>();
        mButton.targetGraphic = mImage;

        button.transform.position = new Vector3(0, 0, 0);
        button.transform.SetParent(content.transform);
        button.GetComponent<Button>().onClick.AddListener(Testmethod);

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
