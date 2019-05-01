using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour{

    #region Variables
    private MainNotification MainNotif;

    #endregion

    // Use this for initialization
    void Start () {
        MainNotif = GetComponent<MainNotification>();
        //notificationDisplayer = GetComponentInParent<NotificationOverview>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Public Functions
    public void OnMouseClick() {
        // Detected click on panel
        Debug.Log("Detected panel click");
        MainNotification mainNotif = GetComponent<MainNotification>();
        Debug.Log("This marker contains " + mainNotif.notifications.Count + " notfifications");
    }

    public void OnMouseEnter() {
        SetActiveMarkerImage();
    }

    public void OnMouseExit() {
        SetInnactiveMarkerImage();
    }
    #endregion

    #region Private Functions 
    private void SetActiveMarkerImage() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
    }

    private void SetInnactiveMarkerImage()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
    }

    #endregion
}
