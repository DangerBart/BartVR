using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualGUI : MonoBehaviour {
    //SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    //ButtonList
    [Header("Add app panels in order from camera, map, menu")]
    [Tooltip("Add app panels in order from camera, map, menu")]
    [SerializeField]
    private List<GameObject> apps = new List<GameObject>();
    //MenuButtons
    [Header("Add app buttons in order from left, top, right, bottom then deselect")]
    [Tooltip("Add app buttons in order from left, top, right, bottom then deselect")]
    [SerializeField]
    private List<GameObject> menuApps = new List<GameObject>();
    //Enums
    private enum App {
        camera = 0,
        map = 1,
        menu = 2,
        none = 3
    }

    private enum Direction {
        left = 0,
        up = 1,
        right = 2,
        down = 3,
        standby = 4
    }

    // Use this for initialization
    void Start() {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        Debug.Log("Index: " + (int)trackedObject.index);

        switch (CurrentApp()) {
            case App.camera:
                RunCamera();
                break;
            case App.map:
                RunMap();
                break;
            case App.menu:
                RunMenu();
                break;
            default:
                //Error: no app active
                break;
        }
    }

    private App CurrentApp() {
        for (int i = 0; i <= apps.Count; i++) {
            if (apps[i].gameObject.activeInHierarchy == true) {
                return (App)i;
            }
        }
        return App.none;
    }

    private void RunCamera() {

    }

    private void RunMap() {

    }

    private void RunMenu() {
        HighlightSelectedApp();
        //App was selected
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && TouchpadDirection(device) != Direction.standby) {
            LaunchApp((int)TouchpadDirection(device));
        }
    }

    void LaunchApp(int app) {
        //Set main menu false
        apps[(int)App.menu].SetActive(false);
        //set selected app true
        apps[app].SetActive(true);
    }

    private void HighlightSelectedApp() {
        //Highlight Finger
        switch (TouchpadDirection(device)) {
            case Direction.left:
                menuApps[0].GetComponent<Button>().Select();
                break;
            case Direction.up:
                menuApps[1].GetComponent<Button>().Select();
                break;
            case Direction.right:
                menuApps[2].GetComponent<Button>().Select();
                break;
            case Direction.down:
                menuApps[3].GetComponent<Button>().Select();
                break;
            case Direction.standby:
                //Highlight a hidden button so none of the visible apps are highlighted
                menuApps[4].GetComponent<Button>().Select();
                break;
        }
    }

    private Direction TouchpadDirection(SteamVR_Controller.Device device) {
        //Get touchpad variables
        float touchpadY = device.GetAxis().y;
        float touchpadX = device.GetAxis().x;

        //Player's finger is in the middle of the touchpad
        if (touchpadY <= 0.75f && touchpadY >= -0.75f) {
            //Player's finger is on the right side of the touchpad
            if (touchpadX > 0.75f) {
                return Direction.right;
            } else if (touchpadX < -0.75f) {
                //Player's finger is on the left side of the touchpad
                return Direction.left;
            }
        } else {
            //Player's hand is on the top side of the touchpad
            if (touchpadY >= 0.75f) {
                return Direction.up;
            } else if (touchpadY <= -0.75f) {
                //Player's finger is on the bottom side of the touchpad
                return Direction.down;
            }
        }
        //If finger is not on touchpad return standby
        return Direction.standby;
    }
}
