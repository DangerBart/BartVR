using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VirtualGUI : MonoBehaviour {
    // HandlerReferences
    private PhotoHandler pHandler;
    private InputHandler iHandler;

    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    // ButtonList
    [Header("Add app panels in order from top to bottom in hierarchy")]
    [SerializeField]
    private List<GameObject> apps = new List<GameObject>();

    // MenuButtons
    [Header("Add app buttons in order from left, top, right, bottom then deselect")]
    [SerializeField]
    private List<GameObject> menuApps = new List<GameObject>();

    // CameraButtons
    [Header("Add app buttons in order of takepicture, back, deselect")]
    [SerializeField]
    private List<GameObject> cameraButton = new List<GameObject>();

    // mapButtons
    [Header("Add app buttons in order of enlargemap, back, deselect")]
    [SerializeField]
    private List<GameObject> mapButton = new List<GameObject>();

    // confirmButtons
    [Header("Add app buttons in order of yes, no, deselect")]
    [SerializeField]
    private List<GameObject> confirmButton = new List<GameObject>();

    // Enums
    private enum App {
        test = 0,
        camera = 1,
        app3 = 2,
        map = 3,
        menu = 4,
        none = 5
    }

    // References to other gameobjects
    [SerializeField]
    private GameObject confirmPanel;
    [SerializeField]
    private GameObject virtualCamera;
    [SerializeField]
    private GameObject preview;

    // Logic variables
    private string path = "";
    private int pictureID = 0;
    private Sprite previewSprite;
    private string pictureRoot = "C:/Users/Vive/Desktop/BARTVR/BartVR/Assets/Resources/Snapshots/";

    // Use this for initialization
    void Start() {
        pHandler = new PhotoHandler();
        iHandler = new InputHandler();
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // ----------------------------TODO------------------------------
    // - Turn switch case into a function because redundancy -- DONE
    // - Finish SendPictureToOC function                     
    // - Clear entire folder after playthrough               -- DONE
    // --------------------------------------------------------------

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        switch (CurrentApp()) {
            case App.camera:
                RunCamera();
                break;
            case App.map:
                RunMap();
                break;
            case App.menu:
                RunMenu();
                break;
            case App.test:
                //RunTest();
                break;
            case App.app3:
                //RunApp3
                break;
        }

        //Camera Pop-up handler
        if (confirmPanel.activeInHierarchy == true) {
            iHandler.Highlight(new List<Direction> { Direction.up, Direction.down, Direction.standby }, confirmButton, device);

            if (iHandler.GetPress(device) == Direction.up) {
                pHandler.SendPictureToOC(pictureID);
            } else if (iHandler.GetPress(device) == Direction.down) {
                confirmPanel.SetActive(false);
            }
        }
    }

    private App CurrentApp() {
        for (int i = 0; i <= apps.Count; i++) {
            if (apps[i].gameObject.activeInHierarchy == true) {
                return (App)i;
            }
        }
        return App.none;
    }

    private void RunCamera() {
        if (confirmPanel.activeInHierarchy == false) {
            
            iHandler.Highlight(new List<Direction> { Direction.left, Direction.right, Direction.standby }, cameraButton, device);

            if (iHandler.GetPress(device) == Direction.left) {
                StartCoroutine(pHandler.TakeScreenShot(virtualCamera, preview, confirmPanel));
            } else if (iHandler.GetPress(device) == Direction.right) {
                ReturnToMenu(App.camera);
            }
        }
    }

    private void RunMap() {

        iHandler.Highlight(new List<Direction> { Direction.up, Direction.down, Direction.standby }, mapButton, device);

        if (iHandler.GetPress(device) == Direction.up) {
            EnlargeMap();
        } else if (iHandler.GetPress(device) == Direction.down) {
            ReturnToMenu(App.map);
        }

    }

    private void RunMenu() {
        //HighlightSelectedApp();
        iHandler.Highlight(new List<Direction> { Direction.left, Direction.up, Direction.right, Direction.down, Direction.standby }, menuApps, device);
        //App was selected
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && iHandler.TouchpadDirection(device) != Direction.standby) {
            LaunchApp((int)iHandler.TouchpadDirection(device));
        }
    }

    private void LaunchApp(int app) {
        // Set main menu false
        apps[(int)App.menu].SetActive(false);
        // set selected app true
        apps[app].SetActive(true);
    }

    private void EnlargeMap() {
        Debug.Log("HA");
    }

    private void ReturnToMenu(App app) {
        apps[(int)app].SetActive(false);
        apps[(int)App.menu].SetActive(true);
    }
}
