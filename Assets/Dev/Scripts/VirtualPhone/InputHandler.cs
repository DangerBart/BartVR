using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Direction enum for access n all classes
public enum Direction {
    left = 0,
    up = 1,
    right = 2,
    down = 3,
    standby = 4
}

public class InputHandler : MonoBehaviour {
    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    // Logic variables
    private float touchMargin = 0.60f;

    private VirtualGUI vGUI = new VirtualGUI();

    // Use this for initialization
    void Start() {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    public void Highlight(List<Direction> directions, List<GameObject> buttons) {
        Direction currentDir = TouchpadDirection(device);
        // Check if current direction of finger on touchpad is in the list of button directions
        if (directions.Contains(currentDir)) 
            // If the finger is in the direction of a button, highlight that button
            buttons[directions.IndexOf(currentDir)].GetComponent<Button>().Select();
    }

    // Return the direction of the press on the touchpad
    public Direction GetPress() {
        Direction currentDir = TouchpadDirection(device);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            return currentDir;
     
        return Direction.standby;
    }

    public Direction TouchpadDirection(SteamVR_Controller.Device device) {
        // Get touchpad variables
        float touchpadY = device.GetAxis().y;
        float touchpadX = device.GetAxis().x;

        // Player's finger is in the middle of the touchpad
        if (touchpadY <= touchMargin && touchpadY >= -touchMargin) {
            // Player's finger is on the right side of the touchpad
            if (touchpadX > touchMargin) {
                return Direction.right;
            } else if (touchpadX < -touchMargin) {
                // Player's finger is on the left side of the touchpad
                return Direction.left;
            }
        } else {
            // Player's hand is on the top side of the touchpad
            if (touchpadY >= touchMargin) {
                return Direction.up;
            } else if (touchpadY <= -touchMargin) {
                // Player's finger is on the bottom side of the touchpad
                return Direction.down;
            }
        }
        // If finger is not on touchpad return standby
        return Direction.standby;
    }
}