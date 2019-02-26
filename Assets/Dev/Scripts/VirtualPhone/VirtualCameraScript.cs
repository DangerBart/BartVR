using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualCameraScript : MonoBehaviour {
    //SteamVR
    private MainMenuScript mm = new MainMenuScript();
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    //AppList
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

        if (this.gameObject.activeInHierarchy) {
            switch (mm.touchpadDirection(device)) {
                case MainMenuScript.Direction.up:
                    buttons[0].GetComponent<Button>().Select();
                    break;
                case MainMenuScript.Direction.down:
                    buttons[1].GetComponent<Button>().Select();
                    break;
                case MainMenuScript.Direction.standby:
                    buttons[2].GetComponent<Button>().Select();
                    break;
            }
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (mm.touchpadDirection(device) == MainMenuScript.Direction.up) {
                takePicture();
            } else if (mm.touchpadDirection(device) == MainMenuScript.Direction.down) {
                Debug.Log("i did a thing");
                mainMenuPanel.SetActive(true);
                this.gameObject.SetActive(false);
            }
        } 
    }

    void takePicture() {
        testText.SetActive(!testText.activeInHierarchy);
    }
}
