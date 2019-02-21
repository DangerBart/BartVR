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
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetHairTriggerDown()) {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit)) {

                if (hit.collider.tag == "VRUIButton") {
                    Debug.Log("Name: " + hit.collider.name);
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();

                } else if (hit.collider.tag == "VRUIToggle") {
                    hit.collider.gameObject.GetComponent<Toggle>().isOn = !(hit.collider.gameObject.GetComponent<Toggle>().isOn);
                }
            }
        }
    }
}
