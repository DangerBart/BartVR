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

    #region Public Functions
    public void OnMouseClick() {
        MainNotification mainNotif = GetComponent<MainNotification>();
        SetSelected(true);
        notificationControl.MarkerClicked(gameObject);
    }

    public void OnMouseEnter() {
        SetActiveMarkerImage(true);
    }

    public void OnMouseExit() {
        if (!selected)
            SetActiveMarkerImage(false);
    }

    public bool GetSelected() {
        return selected;
    }

    public void SetSelected(bool newValue) {
        selected = newValue;
        SetActiveMarkerImage(newValue);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cursor")) {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
            transform.Find("MarkerPanel").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Cursor")) {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
            transform.Find("MarkerPanel").gameObject.SetActive(false);
        }
    }

    #endregion

    #region Private Functions 
    private void SetActiveMarkerImage(bool active) {
        if (active)
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-yellow");
        else
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Notification/location-pointer-purple");
    }

    #endregion
}
