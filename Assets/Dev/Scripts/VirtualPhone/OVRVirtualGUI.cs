using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRVirtualGUI : MonoBehaviour {
    // HandlerReferences
    private PhotoHandler pHandler;
    private OVRInputHandler ovrHandler;

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
    private float cursorMargin = 0.15f;

    //TESTING PURPOSES
    public Text CameraActiveText;


    // Use this for initialization
    void Start() {
        pHandler = new PhotoHandler();
        ovrHandler = new OVRInputHandler();
    }

    // Update is called once per frame
    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
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
        CameraActiveText.gameObject.SetActive(confirmPanel.activeInHierarchy);

        RunCameraPopUp(confirmPanel.activeInHierarchy);
    }

    // MAIN MENU ----


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
        ovrHandler.Highlight(new List<Direction> { Direction.left, Direction.up, Direction.right, Direction.down, Direction.standby }, menuApps);
        //App was selected
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) && ovrHandler.TouchpadDirection() != Direction.standby) {
            LaunchApp((int)ovrHandler.TouchpadDirection());
        }
    }

    private void LaunchApp(int app) {
        // hide current app
        apps[(int)CurrentApp()].SetActive(false);
        // show new app
        apps[app].SetActive(true);
    }

    // MAP APP ----


    private void RunMap() {
        if (ovrHandler.TouchpadIsPressed() && CursorHandler.OnMarker) {
            // CURSOR IS ON MARKER AND A PRESS WAS DETECTED... SELECT THE MARKER
            // Use the public staitc GameObject marker from CursorHandler to extract needed info
        }

        if (GameManager.DesktopMode)
            SnapTo(icon);
        else
            SnapTo(cursor);

        Vector2 finger = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);


        if (!IsBetween(finger.x, -cursorMargin, cursorMargin) || !IsBetween(finger.y, -cursorMargin, cursorMargin)) {
            // Temp is used because C# does not allow for changing a member of a struct returned from a property (localPostion.x or .y)
            Vector3 temp = cursor.transform.localPosition;

            // finger.x is multiplied by 2 since the width is twice as large as the height.
            temp += new Vector3(finger.x * 2f, finger.y, 0) * Time.deltaTime * cursorSpeed;

            temp.x = Mathf.Clamp(temp.x, -1 * contentPanel.rect.width / 2, contentPanel.rect.width / 2);
            temp.y = Mathf.Clamp(temp.y, -1 * contentPanel.rect.height / 2, contentPanel.rect.height / 2);

            cursor.transform.localPosition = temp;
        }
    }

    void SnapTo(RectTransform target) {
        // Get player location on map and set that to be the center of the map view
        contentPanel.anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        contentPanel.anchoredPosition += new Vector2(offsetX, offsetY);
    }

    private bool IsBetween(float val, float low, float high) {
        return val > low && val < high;
    }

    // CAMERA APP ----


    private void RunCamera() {
        if (confirmPanel.activeInHierarchy == false) {
            ovrHandler.Highlight(new List<Direction> { Direction.down, Direction.standby }, cameraButtons);

            if (ovrHandler.GetPress() == Direction.down) {
                Debug.Log("Taking a picture");
                StartCoroutine(pHandler.TakeScreenShot(virtualCamera, preview, confirmPanel));
            }
        }
    }

    private void RunCameraPopUp(bool run) {
        ovrHandler.Highlight(new List<Direction> { Direction.up, Direction.down, Direction.standby }, confirmButtons);

        if (ovrHandler.GetPress() == Direction.up) {
            pHandler.SendPictureToOC();
            confirmPanel.SetActive(false);
        } else if (ovrHandler.GetPress() == Direction.down) {
            confirmPanel.SetActive(false);
        }
    }
}
