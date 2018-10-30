using UnityEngine;

public class Movement : MonoBehaviour {
	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;
	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Vector3 reset = new Vector3(0,0,0);
	private float touchpadY;
	private float touchpadX;

	public float movementSpeed = 2.5f;
	public GameObject playerRig;
	public GameObject camera;


	// Use this for initialization
	void Start() {
		trackedObject = GetComponent<SteamVR_TrackedObject>();
		Debug.Log("Device initialized");
	}

	void Update(){ 
		//Set device equal to the tracked controller
		device = SteamVR_Controller.Input((int)trackedObject.index);
		//Store touchpad y axis in local variable
		touchpadY = device.GetAxis().y;
		touchpadX = device.GetAxis().x;


		//If touchpad is touched in top quarter
		if (device.GetTouch(touchpad)) {
			movePlayer();
		}

		////Reset playerRig position to 0,0,0 when trigger is pressed
		//if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
		//	playerRig.transform.position = reset;
		//}
	}

	//private void movePlayer() { //Based on controller
	//	//Move playerRig based on touchpad axis
	//	playerRig.transform.position += (transform.right * touchpadX + transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
	//	//Reset y position so player does not fly
	//	playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	//}
	private void movePlayer() { //Based on Look
		playerRig.transform.position += (camera.transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}

}