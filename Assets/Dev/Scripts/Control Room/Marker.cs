using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour{

    #region Variables
    private MainNotification MainNotif;
    private NotificationControl notificationControl;
    private bool selected;

    #endregion

    // Use this for initialization
    void Start () {
        MainNotif = GetComponent<MainNotification>();
        notificationControl = FindObjectOfType<NotificationControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Public Functions
    public void OnMouseClick() {
        MainNotification mainNotif = GetComponent<MainNotification>();
        SetSelected(true);
        notificationControl.MarkerClicked(gameObject);
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
