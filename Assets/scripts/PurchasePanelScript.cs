using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelScript : MonoBehaviour
{
	public GameObject purchasePanel;
	// Start is called before the first frame update
    private void Start()
    {
		hidePanel ();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

	public void showPanel()
	{
		purchasePanel.gameObject.SetActive (true);
	}

	public void hidePanel()
	{
		purchasePanel.gameObject.SetActive (false);
	}
}
