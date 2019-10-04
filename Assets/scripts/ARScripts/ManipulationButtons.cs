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

    //TODO: check if an item is selected?

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

    private void OutputDebug(string message)
    {
        GameObject ARDebugLog = GameObject.Find("Debug Log");
        Text DebugLogText = ARDebugLog.GetComponentInChildren<Text>();

        DebugLogText.text = DebugLogText.text + "\n" + message;
        Debug.Log(message);
    }

}
