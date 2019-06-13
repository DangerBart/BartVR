using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRInputHandler : MonoBehaviour {
    // Logic variables
    private float touchMargin = 0.60f;

    /// <summary>
    /// Highlights the given buttons in each direction, buttons[x] will be highlighted by directions[x]
    /// </summary>
    public void Highlight(List<Direction> directions, List<GameObject> buttons) {
        Direction currentDirection = TouchpadDirection();

        // Check if current direction of finger on touchpad is in the list of button directions
        if (directions.Contains(currentDirection)) 
            // Highlight selected button
            buttons[directions.IndexOf(currentDirection)].GetComponent<Button>().Select();
    }

    public bool GetTriggerDown() {
        return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
    }

    public bool TouchpadIsPressed() {
        return (!(GetPress() == Direction.standby) || GetPress() == Direction.standby);
    }

    public Vector2 FingerPositionOnTouchpad() {
        return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
    }

    /// <summary>
    /// Returns direction of a press on the touchpad
    /// </summary>
    public Direction GetPress() {

        Direction currentDirection = TouchpadDirection();

        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            return currentDirection;

        return Direction.standby;
    }

    public Direction TouchpadDirection() {
        // Get touchpad variables
        float touchpadY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        float touchpadX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;

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
