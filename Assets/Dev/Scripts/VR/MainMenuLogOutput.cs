using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainMenuLogOutput : MonoBehaviour {
    public Text logText;

    void OnEnable() {
        Application.logMessageReceived += HandleLog;

        Debug.Log("Current HMD: " + XRDevice.model);
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if (type == LogType.Log || type == LogType.Error)
            logText.text += logString + "\n";
    }
}
