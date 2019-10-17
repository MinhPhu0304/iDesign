using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public GameObject ObjectToPlace;
    public static ItemManager Instance;
    
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //https://www.youtube.com/watch?v=5p2JlI7PV1w
    //https://www.youtube.com/watch?v=AvuuX4qxC_0
}
