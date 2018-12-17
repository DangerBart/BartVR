using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private MinimapControl minimapControl;

    public GameObject defaultNotificationPanel;
    public GameObject defaultBoardPanel;

    //ToDo
    private GameObject selectedNotficationObject;

    void Start() {
        minimapControl = minimap.GetComponent<MinimapControl>();
    }

    public void CreateMessagePanel(Notification notification) {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(defaultNotificationPanel) as GameObject;
        message.SetActive(true);
        
        message.GetComponent<NotificationPanel>().Setup(notification);

        message.transform.SetParent(defaultNotificationPanel.transform.parent, false);

        minimapControl.SetNotificationMinimapLocation(notification);
    }

    public void ToggleFavoritePanel(GameObject originalPanel, Notification notification) {
        //Make a copy of the originalpanel
        GameObject message = Instantiate(originalPanel) as GameObject;
        message.GetComponent<NotificationPanel>().Setup(notification);
        message.SetActive(true);

        //Place it on the boardpanel or the receive notificationpanel
        if(notification.IsFavorite) {
            message.transform.SetParent(defaultBoardPanel.transform.parent, false);
            minimapControl.CreateNewMarker(notification.MinimapLocation);
        }
        else {
            message.transform.SetParent(defaultNotificationPanel.transform.parent, false);
            minimapControl.DeleteSpecifiqMarker(notification.MinimapLocation);
        }
    }

    public void NotificationSelected(GameObject notificationObject) {
        if (selectedNotficationObject != null) {
            selectedNotficationObject.GetComponent<NotificationPanel>().TogglePanelColor();
            //Remove previous selected marker
            minimapControl.DeleteSpecifiqMarker(selectedNotficationObject.GetComponent<NotificationPanel>().GetMinimapLocation());
        }

        if (selectedNotficationObject != notificationObject) {
            notificationObject.GetComponent<NotificationPanel>().TogglePanelColor();
            selectedNotficationObject = notificationObject;

            //Place new minimap marker
            minimapControl.CreateNewMarker(notificationObject.GetComponent<NotificationPanel>().GetMinimapLocation(), true);
        }
        else {
            // Same selected panels
            selectedNotficationObject = null;
        }
    }

    public void NotificationPanelRemoved(Vector2 minimapLocation) {
        minimapControl.DeleteSpecifiqMarker(minimapLocation); 
    }
}
