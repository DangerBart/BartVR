using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;
	private Vector3 reset = new Vector3(0,0,0);

	public float movementSpeed = 2.5f;
	public GameObject playerRig;
	public GameObject reference;


	// Use this for initialization
	void Start() {
		trackedObject = GetComponent<SteamVR_TrackedObject>();
		Debug.Log("Device initialized");
	}

	void Update(){ 
		//Set device equal to the tracked controller
		device = SteamVR_Controller.Input((int)trackedObject.index);
		//Store touchpad y axis in local variable
		float touchpadY = device.GetAxis().y;

		//If touchpad is touched in top quarter
		if (touchpadY >= 0.5) {
			movePlayer();
		}

		//Reset playerRig position to 0,0,0 when trigger is pressed
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			playerRig.transform.position = reset;
		}
	}

	private void movePlayer() {
		//Move playerRig forward in direction of camera
		playerRig.transform.position += reference.transform.forward * Time.deltaTime;
		//Reset y position so player does not fly
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}
}