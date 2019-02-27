using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualCameraScript : MonoBehaviour {
    //SteamVR
    private MainMenuScript mm = new MainMenuScript();
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    //ButtonList
    [Header("Add buttons in order from top to bottom and finally deselect")]
    [Tooltip("Add buttons in order from top to bottom and finally deselect")]
    [SerializeField]
    private List<GameObject> buttons = new List<GameObject>();
    //MainMenuPanel reference
    [SerializeField]
    private GameObject mainMenuPanel;

    //todo test
    [SerializeField]
    private GameObject testText;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        //Highlight button in direction of finger on touchpad
        if (this.gameObject.activeInHierarchy) {
            switch (mm.TouchpadDirection(device)) {
                case MainMenuScript.Direction.up:
                    Debug.Log(buttons[0].gameObject.name);
                    buttons[0].GetComponent<Button>().Select();
                    break;
                case MainMenuScript.Direction.down:
                    Debug.Log(buttons[1].gameObject.name);
                    buttons[1].GetComponent<Button>().Select();
                    break;
                case MainMenuScript.Direction.standby:
                    buttons[2].GetComponent<Button>().Select();
                    break;
            }
        }
        //Detect which button was clicked
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (mm.TouchpadDirection(device) == MainMenuScript.Direction.up) {
                TakePicture();
            } else if (mm.TouchpadDirection(device) == MainMenuScript.Direction.down) {
                mainMenuPanel.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    void TakePicture() {
        testText.SetActive(!testText.activeInHierarchy);
    }
}
