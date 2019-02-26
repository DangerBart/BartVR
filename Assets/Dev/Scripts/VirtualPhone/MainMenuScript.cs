using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    //SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float touchpadY;
    private float touchpadX;
    //AppList
    [Header ("Add apps in order of left>top>right>bottom>deselect")]
    [Tooltip("Add apps in order of left>top>right>bottom>deselect")]
    [SerializeField]
    private List<GameObject> apps = new List<GameObject>();
    //PanelList
    [Header("Add panels in order of testGUI>camera>unnamed>map")]
    [Tooltip("Add panels in order of testGUI>camera>unnamed>map")]
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();
    //Direction enum
    enum Direction{
        left = 0,
        up = 1,
        right = 2,
        down = 3,
        standby = 4
    }


    // Use this for initialization
    void Start () {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        //Highlight app in direction of finger on touchpad
        switch (touchpadDirection()) {
            case Direction.left:
                apps[0].GetComponent<Button>().Select();
                break;
            case Direction.up:
                apps[1].GetComponent<Button>().Select();
                break;
            case Direction.right:
                apps[2].GetComponent<Button>().Select();
                break;
            case Direction.down:
                apps[3].GetComponent<Button>().Select();
                break;
            case Direction.standby:
                //Highlight a hidden button so none of the visible apps are highlighted
                apps[4].GetComponent<Button>().Select();
                break;
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && touchpadDirection() == Direction.standby) {
            launchApp((int)touchpadDirection());
        }
    }

    void launchApp(int app) {
        panels[app].SetActive(true);
    }

    Direction touchpadDirection() {
        //Get touchpad variables
        touchpadY = device.GetAxis().y;
        touchpadX = device.GetAxis().x;

        //Player's finger is in the middle of the touchpad
        if (touchpadY <= 0.75 && touchpadY >= -0.75) {
            //Player's finger is on the right side of the touchpad
            if (touchpadX > 0.75) {
                return Direction.right;
            } else if (touchpadX < -0.75) {
                //Player's finger is on the left side of the touchpad
                return Direction.left;
            }
        } else {
            //Player's hand is on the top side of the touchpad
            if (touchpadY >= 0.75) {
                return Direction.up;
            } else if (touchpadY <= -0.75) {
                //Player's finger is on the bottom side of the touchpad
                return Direction.down;
            }
        }
        return Direction.standby;
    }
}
