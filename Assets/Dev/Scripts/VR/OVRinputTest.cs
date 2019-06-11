using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRinputTest : MonoBehaviour {
    private string[] controllers;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text length;

    [SerializeField]
    private Text trigger;

    // Use this for initialization
    void Start () {
        controllers = Input.GetJoystickNames();

        foreach (string controller in controllers) {
            text.text += controller + "\n";
        }
    }
	
	// Update is called once per frame
	void Update () {
        length.text = controllers.Length.ToString();

        trigger.gameObject.SetActive(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger));        
    }
}
