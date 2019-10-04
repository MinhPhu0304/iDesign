using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AROutputLog : MonoBehaviour
{
    public GameObject textLog;

    public void OutputDebug(string message)
    {
        Text DebugLogText = textLog.GetComponentInChildren<Text>();

        DebugLogText.text = DebugLogText.text + "\n" + message;
        Debug.Log(message);
    }
}
