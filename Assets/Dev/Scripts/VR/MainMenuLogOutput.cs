using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLogOutput : MonoBehaviour {
    public Text logText;
    public Text NPCText;

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if (type == LogType.Log || type == LogType.Error)
            logText.text += logString + "\n";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        NPCText.text = GameManager.amountOfNpcsToSpawn.ToString();
	}
}
