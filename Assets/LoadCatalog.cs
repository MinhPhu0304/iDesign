﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;

public class LoadCatalog : MonoBehaviour
{
    Resolution[] resolutions;

    string[] CataLogTitle = { "Chair", "Beds", "Couch", "Coffee table", "Dining table", "Coat rack" };

    [SerializeField] GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject content = GameObject.Find("Content");
        resolutions = Screen.resolutions;
        for( int i = 0; i < CataLogTitle.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = CataLogTitle[i];
            button.GetComponent<Button>().onClick.AddListener(Testmethod);
            button.transform.SetParent(content.transform);
            Vector3 position = new Vector3(0 ,i * 30 - 300);
            button.transform.position = position;

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
