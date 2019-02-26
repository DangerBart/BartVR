using UnityEngine;

public class Movement : MonoBehaviour {
    //SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Vector3 reset = new Vector3();
    private float touchpadY;
    private float touchpadX;

    //Movement
    public float movementSpeed = 2.5f;
    public GameObject playerRig;
    public GameObject cam;
    public GameObject cameraEye;
    private int movementSwitch;

    //Collisions
    private bool isNoCollision = true;
    private Vector3 movement;
    private Vector3 newPosition;
    private Vector3 prevPos;
    private Vector3 fwd;

    // Use this for initialization
    void Start() {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        movementSwitch = Gamemanager.movementValue + 1;
    }

    void Update() {
        // Set device equal to the tracked controller
        device = SteamVR_Controller.Input((int)trackedObject.index);
        //Store touchpad y axis in local variable
        touchpadY = device.GetAxis().y;
        touchpadX = device.GetAxis().x;
        

        if (isNoCollision || touchpadY < 0) {
            // If player is touching movement controls
            MovePlayer();
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
                default:
                    MovePlayerTeleport();
                    break;
            }
        }
    }

    private void MovePlayerBasedOnLook() { 
        cameraEye.GetComponent<TeleportVive>().enabled = false;
        playerRig.transform.position += cam.transform.forward * touchpadY * Time.deltaTime * movementSpeed;
        //Reset y position so player does not fly
        playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
    }
    
    private void MovePlayerBasedOnController() { 
        cameraEye.GetComponent<TeleportVive>().enabled = false;
        playerRig.transform.position += (transform.right * touchpadX + transform.forward * touchpadY) * Time.deltaTime * movementSpeed;
        //Reset y position so player does not fly
        playerRig.transform.position = new Vector3(playerRig.transform.position.x, 0, playerRig.transform.position.z);
    }
    
    private void MovePlayerTeleport() {
        cameraEye.GetComponent<TeleportVive>().enabled = true;
    }

    //Because Unity uses this as a measure of detecting collisions we have to use this
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "POI")
        {
            isNoCollision = false;
        }
    }
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag != "POI")
        {
            isNoCollision = false;
        }
    }
    void OnCollisionExit(Collision collision) {
        isNoCollision = true;
    }
}