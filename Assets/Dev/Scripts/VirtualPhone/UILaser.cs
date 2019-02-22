using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILaser : MonoBehaviour {
    
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        //Get reference to controller through SteamVR_TrackedObject, 
        //rather than SteamVR_TrackedController for access to more functions
        device = SteamVR_Controller.Input((int)trackedObject.index);

        //Check if the trigger was pressed
        if (device.GetHairTriggerDown()) {
            //Send out a ray in the forward direction
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            //Check what the ray hit
            if (Physics.Raycast(transform.position, fwd, out hit)) {
                //If ray hit a button trigger the button's function
                if (hit.collider.tag == "VRUIButton") {
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();

                } 
            }
        }
    }
}
