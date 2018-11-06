using UnityEngine;

public class Movement : MonoBehaviour {
	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;
	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Vector3 reset = new Vector3(0, 0, 0);
	private float touchpadY;
	private float touchpadX;

	public float movementSpeed = 2.5f;
	public GameObject playerRig;
	public GameObject cam;

	public int movementSwitch = 1;
	//Collisions
	private bool isNoCollison = true;
	private Vector3 movement;
	private Vector3 newPosition;
	private Vector3 prevPos;
	private Vector3 fwd;

	// Use this for initialization
	void Start() {
		trackedObject = GetComponent<SteamVR_TrackedObject>();
		Debug.Log("Device initialized");
	}
	void Update() {
		//Set device equal to the tracked controller
		device = SteamVR_Controller.Input((int)trackedObject.index);
		//Store touchpad y axis in local variable
		touchpadY = device.GetAxis().y;
		touchpadX = device.GetAxis().x;

		newPosition = playerRig.transform.position;
		movement = (newPosition - prevPos);

		if (isNoCollison) {
			//If touchpad is touched in top quarter
			movePlayer();
		} else {
			if (touchpadY < 0) { //IF PLAYER IS MOVING BACKWARDS
				movePlayer();
			} else {
				Debug.Log("STILL STUCK");
			}
		}
	}
	private void LateUpdate() {
		prevPos = playerRig.transform.position;
		fwd = playerRig.transform.forward;
		isNoCollison = true;
	}
	private void movePlayer() {
		if (device.GetTouch(touchpad)) {
			switch (movementSwitch) {
				case 1:
					movePlayerBasedOnLook();
					break;
				case 2:
					movePlayerBasedOnController();
					break;
				default:
					movePlayerBasedOnLook();
					break;
			}
		}
	}
	//...1...//
	private void movePlayerBasedOnLook() { //Based on Look
		playerRig.transform.position += (cam.transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}

	//...2...//
	private void movePlayerBasedOnController() { //Based on controller
												 //Move playerRig based on touchpad axis
		playerRig.transform.position += (transform.right * touchpadX + transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
		//Reset y position so player does not fly
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}
	void OnCollisionEnter(Collision collision) {
		isNoCollison = false;
		Debug.Log("HAND COLLISION");
	}
	void OnCollisionStay(Collision collision) {
		Debug.Log("STILL HAND COLLISION");
		isNoCollison = false;
	}
	void OnCollisionExit(Collision collision) {
		isNoCollison = true;
		Debug.Log("NO HAND COLLISION");
	}
}
////Reset playerRig position to 0,0,0 when trigger is pressed
//if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
//	playerRig.transform.position = reset;
//}