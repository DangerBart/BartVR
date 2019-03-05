﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualGUI : MonoBehaviour {
    //SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    //ButtonList
    [Header("Add app panels in order from top to bottom in hierarchy")]
    [Tooltip("Add app panels in order from top to bottom in hierarchy")]
    [SerializeField]
    private List<GameObject> apps = new List<GameObject>();
    //MenuButtons
    [Header("Add app buttons in order from left, top, right, bottom then deselect")]
    [Tooltip("Add app buttons in order from left, top, right, bottom then deselect")]
    [SerializeField]
    private List<GameObject> menuApps = new List<GameObject>();
    //CameraButtons
    [Header("Add app buttons in order of takepicture, back, deselect")]
    [Tooltip("Add app buttons in order of takepicture, back, deselect")]
    [SerializeField]
    private List<GameObject> cameraButton = new List<GameObject>();
    //mapButtons
    [Header("Add app buttons in order of enlargemap, back, deselect")]
    [Tooltip("Add app buttons in order of enlargemap, back, deselect")]
    [SerializeField]
    private List<GameObject> mapButton = new List<GameObject>();
    //Enums
    private enum App {
        test = 0,
        camera = 1,
        app3 = 2,
        map = 3,
        menu = 4,
        none = 5
    }

    private enum Direction {
        left = 0,
        up = 1,
        right = 2,
        down = 3,
        standby = 4
    }

    //Logic variables
    private float touchpadMargin = 0.60f;

    // Use this for initialization
    void Start() {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);

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
            case App.test:
                //RunTest();
                break;
            case App.app3:
                //RunApp3
                break;
            default:
                //Error: no app active
                Debug.LogError("NO ACTIVE APP ON GUI");
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
        switch (TouchpadDirection(device)) {
            case Direction.left:
                cameraButton[0].GetComponent<Button>().Select();
                break;
            case Direction.right:
                cameraButton[1].GetComponent<Button>().Select();
                break;
            case Direction.standby:
                cameraButton[2].GetComponent<Button>().Select();
                break;
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if(TouchpadDirection(device) == Direction.left) {
                TakePicture();
            } else if (TouchpadDirection(device) == Direction.right) {
                //return to main menu
                apps[(int)App.camera].SetActive(false);
                apps[(int)App.menu].SetActive(true);
            }
        }
    }

    private void RunMap() {
        switch (TouchpadDirection(device)) {
            case Direction.up:
                mapButton[0].GetComponent<Button>().Select();
                break;
            case Direction.down:
                mapButton[1].GetComponent<Button>().Select();
                break;
            case Direction.standby:
                mapButton[2].GetComponent<Button>().Select();
                break;
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (TouchpadDirection(device) == Direction.up) {
                EnlargeMap();
            } else if (TouchpadDirection(device) == Direction.down) {
                //return to main menu
                apps[(int)App.map].SetActive(false);
                apps[(int)App.menu].SetActive(true);
            }
        }
    }

    private void RunMenu() {
        HighlightSelectedApp();
        //App was selected
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && TouchpadDirection(device) != Direction.standby) {
            LaunchApp((int)TouchpadDirection(device));
        }
    }

    private void LaunchApp(int app) {
        //Set main menu false
        apps[(int)App.menu].SetActive(false);
        //set selected app true
        apps[app].SetActive(true);
    }

    private void TakePicture() {
        Debug.Log("HA");
    }

    private void EnlargeMap() {
        Debug.Log("HA");
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
        if (touchpadY <= touchpadMargin && touchpadY >= -touchpadMargin) {
            //Player's finger is on the right side of the touchpad
            if (touchpadX > touchpadMargin) {
                return Direction.right;
            } else if (touchpadX < -touchpadMargin) {
                //Player's finger is on the left side of the touchpad
                return Direction.left;
            }
        } else {
            //Player's hand is on the top side of the touchpad
            if (touchpadY >= touchpadMargin) {
                return Direction.up;
            } else if (touchpadY <= -touchpadMargin) {
                //Player's finger is on the bottom side of the touchpad
                return Direction.down;
            }
        }
        //If finger is not on touchpad return standby
        return Direction.standby;
    }
}
