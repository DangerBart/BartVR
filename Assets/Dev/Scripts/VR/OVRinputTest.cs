using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRinputTest : MonoBehaviour {
    private string[] controllers;

    public Text logText;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text length;

    private OVRInputHandler ovrHandler;

    public Text TriggerDownCalled;
    public Text FingerX;
    public Text FingerY;
    public Text GetPressText;
    public Text TouchpadDirectionText;

    [SerializeField]
    private Text trigger;

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    // Use this for initialization
    void Start () {
        controllers = Input.GetJoystickNames();

        ovrHandler = new OVRInputHandler();

        foreach (string controller in controllers) {
            text.text += controller + "\n";
        }
    }
	
	// Update is called once per frame
	void Update () {
        length.text = controllers.Length.ToString();

        trigger.gameObject.SetActive(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger));

        TriggerDownCalled.gameObject.SetActive(ovrHandler.GetTriggerDown());
        FingerX.text = ovrHandler.FingerPositionOnTouchpad().x.ToString();
        FingerY.text = ovrHandler.FingerPositionOnTouchpad().y.ToString();
        GetPressText.text = ovrHandler.GetPress().ToString();
        TouchpadDirectionText.text = ovrHandler.TouchpadDirection().ToString();
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if (type == LogType.Log)
            logText.text += logString + "\n";
    }
}
