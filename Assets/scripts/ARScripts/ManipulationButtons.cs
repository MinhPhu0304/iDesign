using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulationButtons : MonoBehaviour
{

    public bool toggleRotate = false;
    public bool toggleMove = false;
    public bool togglePlace = true;
    public bool toggleScale = false;
    public bool toggleLift = false;

    public Color toggledColour;

    private List<GameObject> toggleButtons;
    public GameObject rotateButton;
    public GameObject moveButton;
    public GameObject placeButton;
    public GameObject deleteButton;
    public GameObject ItemPanel;

    private void Start()
    {
        toggleButtons = new List<GameObject>();
        toggleButtons.Add(rotateButton);
        toggleButtons.Add(moveButton);
        toggleButtons.Add(placeButton);

        TogglePressedColour();
        ItemPanel.SetActive(false);
    }

    public void RotatePressed() {
        if (toggleRotate)
        {
            toggleRotate = false;
        }
        else
        {
            toggleMove = false;
            togglePlace = false;
            toggleScale = false;
            toggleRotate = true;
            
        }
        TogglePressedColour();

        OutputDebug("Rotate set to: " + toggleRotate);
    }

    public void MovePressed() {
        if (toggleMove)
        {
            toggleMove = false;
        }
        else
        {
            toggleRotate = false;
            togglePlace = false;
            toggleScale = false;
            toggleLift = false;
            toggleMove = true;
            
        }
        TogglePressedColour();

        OutputDebug("Move set to: " + toggleMove);

    }

    public void PlacePressed() {
        if (togglePlace)
        {
            togglePlace = false;
        }
        else
        {
            toggleRotate = false;
            toggleMove = false;
            toggleScale = false;
            toggleLift = false;
            togglePlace = true;
            
        }
        TogglePressedColour();

        OutputDebug("Place set to: " + togglePlace);
    }

    public void ScalePressed() {
        if (toggleScale)
        {
            toggleScale = false;
        }
        else
        {
            toggleRotate = false;
            toggleMove = false;
            togglePlace = false;
            toggleLift = false;
            toggleScale = true;
        }

        OutputDebug("Scale set to: " + toggleScale);
    }

    public void LiftPressed()
    {
        if (toggleLift)
        {
            toggleScale = false;
        }
        else
        {
            toggleRotate = false;
            toggleMove = false;
            togglePlace = false;
            toggleScale = false;
            toggleLift = true;
        }

        OutputDebug("Scale set to: " + toggleScale);
    }

    public bool GetRotationStatus() {
        return toggleRotate;
    }

    public bool GetMoveStatus() {
        return toggleMove;
    }

    public bool GetPlaceStatus() {
        return togglePlace;
    }

    public bool GetScaleStatus() {
        return toggleScale;
    }
    public bool GetLiftStatus() {
        return toggleLift;
    }

    public void TogglePressedColour()
    {
        foreach (GameObject toggleButton in toggleButtons)
        {
            if (toggleButton == rotateButton && toggleRotate)
            {
                toggleButton.GetComponent<Image>().color = toggledColour;
            }
            else if (toggleButton == moveButton && toggleMove)
            {
                toggleButton.GetComponent<Image>().color = toggledColour;
            }
            else if (toggleButton == placeButton && togglePlace)
            {
                toggleButton.GetComponent<Image>().color = toggledColour;
                ItemPanel.SetActive(true);
            }
            else
            {
                toggleButton.GetComponent<Image>().color = Color.white;
            }

            if (togglePlace == false)
            {
                ItemPanel.SetActive(false);
            }
        }
    }

    private void OutputDebug(string message)
    {
        GameObject ARDebugLog = GameObject.Find("Debug Log");
        Text DebugLogText = ARDebugLog.GetComponentInChildren<Text>();

        DebugLogText.text = DebugLogText.text + "\n" + message;
        Debug.Log(message);
    }

}
