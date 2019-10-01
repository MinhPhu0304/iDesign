using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulationButtons : MonoBehaviour
{

    static public bool toggleRotate = false;
    static public bool toggleMove = false;

    //TODO: check if an item is selected?

    public void RotatePressed() {
        if (toggleRotate)
        {
            toggleRotate = false;
        }
        else
        {
            toggleMove = false;
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
            toggleMove = true;
        }
        OutputDebug("Move set to: " + toggleMove);

    }

    public bool GetRotationStatus() {
        return toggleRotate;
    }

    public bool GetMoveStatus() {
        return toggleMove;
    }

    private void OutputDebug(string message)
    {
        GameObject ARDebugLog = GameObject.Find("Debug Log");
        Text DebugLogText = ARDebugLog.GetComponentInChildren<Text>();

        DebugLogText.text = DebugLogText.text + "\n" + message;
        Debug.Log(message);
    }

}
