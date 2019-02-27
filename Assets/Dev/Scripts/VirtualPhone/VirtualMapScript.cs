using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualMapScript : MonoBehaviour {
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

    // Use this for initialization
    void Start () {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (this.gameObject.activeInHierarchy) {
            //Highlight buttons
            if (this.gameObject.activeInHierarchy) {
                switch (mm.TouchpadDirection(device)) {
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
            //Check if touchpad was pressed
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
                if (mm.TouchpadDirection(device) == MainMenuScript.Direction.up) {
                    EnlargeMap();
                } else if (mm.TouchpadDirection(device) == MainMenuScript.Direction.down) {
                    mainMenuPanel.SetActive(true);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    //Custom functions
    void EnlargeMap() {
        Debug.Log("patience my child");
    }
}
