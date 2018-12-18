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
            if (!notification.IsSelected)
            {
                minimapControl.CreateNewMarker(notification.MinimapLocation);
            }
        }
        else {
            message.transform.SetParent(defaultNotificationPanel.transform.parent, false);
            minimapControl.DeleteSpecifiqMarker(notification.MinimapLocation);
        }

        if (notification.IsSelected) {
            selectedNotficationObject = message;
        }
    }

    public void NotificationSelected(GameObject notificationObject) {
        NotificationPanel notificationPanel = notificationObject.GetComponent<NotificationPanel>();

        if (selectedNotficationObject != null) {
            NotificationPanel selectedNotificationPanel = selectedNotficationObject.GetComponent<NotificationPanel>();

            selectedNotificationPanel.TogglePanelColor();
            //Remove previous selected marker
            minimapControl.DeleteSpecifiqMarker(selectedNotificationPanel.GetMinimapLocation());

            if (selectedNotificationPanel.IsFavorite())
            {
                minimapControl.CreateNewMarker(selectedNotificationPanel.GetMinimapLocation());
            }
        }

        if (notificationPanel.IsFavorite()) {
            minimapControl.DeleteSpecifiqMarker(notificationPanel.GetMinimapLocation());
        }

        if (selectedNotficationObject != notificationObject) {
            // Currently selected panel an previous are not the same

            notificationPanel.TogglePanelColor();
            selectedNotficationObject = notificationObject;

            //Place new minimap marker
            minimapControl.CreateNewMarker(notificationPanel.GetMinimapLocation(), true);
        }
        else {
            // Selected panel was clicked twice
            if (notificationPanel.IsFavorite()) {
                minimapControl.CreateNewMarker(notificationPanel.GetMinimapLocation());
            }

            selectedNotficationObject = null;
        }
    }

    public void NotificationPanelRemoved(Vector2 minimapLocation) {
        minimapControl.DeleteSpecifiqMarker(minimapLocation);
    }
}
