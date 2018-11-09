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
	public GameObject cameraEye;

	public int movementSwitch = 3; //enum
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

		if (isNoCollison) {
			//If player is touching movement controls
			MovePlayer();
		} else {
			if (touchpadY < 0) { //Player can only move backwards
				MovePlayer();
			} 
		}
	}
	private void MovePlayer() {
		if (device.GetTouch(touchpad)) {
			switch (movementSwitch) {
				case 1:
					MovePlayerBasedOnLook();
					break;
				case 2:
					MovePlayerBasedOnController();
					break;
                case 3:
                    MovePlayerTeleport();
                    break;
                default:
					MovePlayerTeleport();
					break;
			}
		}
	}
	//...1...//
	private void MovePlayerBasedOnLook() { //Based on direction players is looking at
		cameraEye.GetComponent<TeleportVive>().enabled = false;
		playerRig.transform.position += (cam.transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}

	//...2...//
	private void MovePlayerBasedOnController() { //Move playerRig based on touchpad axis
		cameraEye.GetComponent<TeleportVive>().enabled = false;
		playerRig.transform.position += (transform.right * touchpadX + transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
		//Reset y position so player does not fly
		playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
	}

	//...3...///
	private void MovePlayerTeleport() {
		cameraEye.GetComponent<TeleportVive>().enabled = true;
	}

	void OnCollisionEnter(Collision collision) {
		isNoCollison = false;
	}
	void OnCollisionStay(Collision collision) {
		isNoCollison = false;
	}
	void OnCollisionExit(Collision collision) {
		isNoCollison = true;
	}
}