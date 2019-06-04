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
    // Logic variables
    private float touchMargin = 0.60f;

    private VirtualGUI vGUI = new VirtualGUI();
    
    /// <summary>
    /// Highlights the given buttons in each direction, buttons[x] will be highlighted by directions[x]
    /// </summary>
    public void Highlight(List<Direction> directions, List<GameObject> buttons, SteamVR_Controller.Device device) {
        Direction currentDirection = TouchpadDirection(device);
        
        // Check if current direction of finger on touchpad is in the list of button directions
        if (directions.Contains(currentDirection)) 
            // Highlight selected button
            buttons[directions.IndexOf(currentDirection)].GetComponent<Button>().Select();
    }
    
    public bool GetTriggerDown(SteamVR_Controller.Device device) {
        return device.GetHairTriggerDown();
    }

    public bool TouchpadIsPressed(SteamVR_Controller.Device device) {
        return (!(GetPress(device) == Direction.standby) || GetPress(device) == Direction.standby);
    }

    public Vector2 FingerPositionOnTouchpad(SteamVR_Controller.Device device) {
        return device.GetAxis();
    }

    /// <summary>
    /// Returns direction of a press on the touchpad of device
    /// </summary>
    public Direction GetPress(SteamVR_Controller.Device device) {

        Direction currentDirection = TouchpadDirection(device);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            return currentDirection;
     
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