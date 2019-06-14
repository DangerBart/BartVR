using UnityEngine;
using UnityEngine.UI;

public class OVRUILaser : MonoBehaviour {

    OVRInputHandler ovrHandler;

	// Use this for initialization
	void Start () {
        ovrHandler = new OVRInputHandler();
	}
	
	// Update is called once per frame
	void Update () {
        //Check if the trigger was pressed
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {
            //Send out a ray in the forward direction
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            //Check what the ray hit
            if (Physics.Raycast(transform.position, fwd, out hit)) {
                //If ray hit a button, trigger the button's onClick function
                if (hit.collider.tag == "VRUIButton") {
                    hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }
}
