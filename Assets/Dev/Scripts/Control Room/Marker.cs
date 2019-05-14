using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour{

    #region Variables
    private MainNotification MainNotif;
    private NotificationOverview notifOverview;
    private NotificationControl notificationControl;
    private bool selected;

    #endregion

    // Use this for initialization
    void Start () {
        MainNotif = GetComponent<MainNotification>();
        notifOverview = FindObjectOfType<NotificationOverview>();
        notificationControl = FindObjectOfType<NotificationControl>();

        if (notifOverview)
            Debug.Log("GUITexture object found: " + notifOverview.name);
        else
            Debug.Log("No GUITexture object could be found");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Public Functions
    public void OnMouseClick() {
        MainNotification mainNotif = GetComponent<MainNotification>();
        SetSelected(true);

        Debug.Log(mainNotif.keyNote);
        notificationControl.MarkerClicked(gameObject);
    }

    public void SetNotifOverview(NotificationOverview notifOverview) {
        this.notifOverview = notifOverview;
    }

    public void OnMouseEnter() {
        SetActiveMarkerImage();
    }

    public void OnMouseExit() {
        if (!selected)
            SetInnactiveMarkerImage();
    }

    public bool GetSelected() {
        return selected;
    }

    public void SetSelected(bool newValue) {
        selected = newValue;

        if (newValue)
            SetActiveMarkerImage();
        else
            SetInnactiveMarkerImage();

    }

    #endregion

    #region Private Functions 
    private void SetActiveMarkerImage() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
    }

    private void SetInnactiveMarkerImage() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
    }

    #endregion
}
