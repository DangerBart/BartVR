using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILaser : MonoBehaviour {

    private bool singlePress = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();

        if (controller.triggerPressed && !singlePress) {
            singlePress = true;
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit)) {
                Debug.Log("Hit : " + hit.collider.tag);
                Debug.DrawRay(transform.position, fwd * hit.distance, Color.yellow);
                if (hit.collider.tag == "VRUI") {
                    Debug.Log("Name: " + hit.collider.name);
                    hit.collider.gameObject.GetComponent<VRButtonInteractTest>().buttonTest();
                }
            }
            singlePress = false;
        }
    }
}
