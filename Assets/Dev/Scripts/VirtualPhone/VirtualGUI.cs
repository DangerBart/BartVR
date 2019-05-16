using System.Collections.Generic;
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
    [Header("Add app buttons in order of back, takepicture, deselect")]
    [SerializeField]
    private List<GameObject> cameraButtons = new List<GameObject>();

    // ConfirmButtons
    [Header("Add app buttons in order of yes, no, deselect")]
    [SerializeField]
    private List<GameObject> confirmButtons = new List<GameObject>();

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
    [SerializeField]
    private GameObject largeMap;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private RectTransform icon;
    [SerializeField]
    private RectTransform cursor;

    // Logic variables
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    private float cursorSpeed = 35f;

    // Use this for initialization
    void Start() {
        pHandler = new PhotoHandler();
        iHandler = new InputHandler();
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (iHandler.GetTriggerDown(device))
            if (confirmPanel.activeInHierarchy)
                confirmPanel.SetActive(false);
            else 
                ReturnToMenu(CurrentApp());

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
                //TODO third panel;
                ReturnToMenu(App.test);
                break;
            case App.app3:
                //TODO fourth panel
                ReturnToMenu(App.app3);
                break;
            case App.none:
                ReturnToMenu(App.menu);
                break;
        }



        RunCameraPopUp(confirmPanel.activeInHierarchy);
    }

    // MAIN MENU

    private App CurrentApp() {
        for (int i = 0; i <= apps.Count; i++) {
            if (apps[i].gameObject.activeInHierarchy == true) {
                return (App)i;
            }
        }
        return App.none;
    }

    private void ReturnToMenu(App app) {
        if (app != App.menu) {
            apps[(int)app].SetActive(false);
            apps[(int)App.menu].SetActive(true);
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

    // MAP APP

    private void RunMap() {
        // center view on player
        //SnapTo(icon);
        SnapTo(cursor);


        Vector2 finger = iHandler.FingerPositionOnTouchpad(device);
        // Temp is used because C# does not allow for changing a member of a struct returned from a property (localPostion.x or .y)
        Vector3 temp = cursor.transform.localPosition;

        // finger.x is multiplied by 2 since the width is twice as large as the height.
        temp += new Vector3(finger.x * 2f, finger.y, 0) * Time.deltaTime * cursorSpeed;

        temp.x = Mathf.Clamp(temp.x, -1 * contentPanel.rect.width / 2, contentPanel.rect.width / 2);
        temp.y = Mathf.Clamp(temp.y, -1 * contentPanel.rect.height / 2, contentPanel.rect.height / 2);

        cursor.transform.localPosition = temp;
    }

    void SnapTo(RectTransform target) {
        // Get player location on map and set that to be the center of the map view
        contentPanel.anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        contentPanel.anchoredPosition += new Vector2(offsetX, offsetY);
    }

    // CAMERA APP

    private void RunCamera() {
        if (confirmPanel.activeInHierarchy == false) {
            iHandler.Highlight(new List<Direction> { Direction.down, Direction.standby }, cameraButtons, device);

            if (iHandler.GetPress(device) == Direction.down) {
                StartCoroutine(pHandler.TakeScreenShot(virtualCamera, preview, confirmPanel));
            }
        }
    }

    private void RunCameraPopUp(bool run) {
        iHandler.Highlight(new List<Direction> { Direction.up, Direction.down, Direction.standby }, confirmButtons, device);

        if (iHandler.GetPress(device) == Direction.up) {
            pHandler.SendPictureToOC();
            confirmPanel.SetActive(false);
        } else if (iHandler.GetPress(device) == Direction.down) {
            confirmPanel.SetActive(false);
        }
    }
}
