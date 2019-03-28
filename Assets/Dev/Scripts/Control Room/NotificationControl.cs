﻿using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private MinimapControl minimapControl;

    [SerializeField]
    private GameObject relevantNotificationPanel;
    [SerializeField]
    private GameObject postableNotificationPanel;
    [SerializeField]
    private GameObject favoritePanel;

    private GameObject selectedNotficationObject;

    void Start() {
        minimapControl = minimap.GetComponent<MinimapControl>();
    }

    public void CreateRelevantMessagePanel(DLinkedList notification) {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(relevantNotificationPanel) as GameObject;
        message.SetActive(true);
        
        message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Relevant);

        message.transform.SetParent(relevantNotificationPanel.transform.parent, false);

        minimapControl.SetNotificationMinimapLocation(notification.GetData());
    }

    public void CreatePostableMessagePanel(DLinkedList notification)
    {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(postableNotificationPanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Postable);

        message.transform.SetParent(postableNotificationPanel.transform.parent, false);

        minimapControl.SetNotificationMinimapLocation(notification.GetData());
    }

    public void ToggleFavoritePanel(GameObject originalPanel, DLinkedList notification) {
        //Make a copy of the originalpanel
        GameObject message = Instantiate(originalPanel) as GameObject;
        message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Relevant);
        message.SetActive(true);

        //Place it on the boardpanel or the receive notificationpanel
        if(notification.GetData().IsFavorite) {
            message.transform.SetParent(relevantNotificationPanel.transform.parent, false);

            if (!notification.GetData().IsSelected) {
                // Create marker when notfication is not selected
                minimapControl.CreateNewMarker(notification.GetData().MinimapLocation);
            }
        }
        else {
            message.transform.SetParent(relevantNotificationPanel.transform.parent, false);
            minimapControl.DeleteSpecifiqMarker(notification.GetData().MinimapLocation);
        }

        if (notification.GetData().IsSelected) {
            // Making sure selectedNotificationObject is updated
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
                // As the selected marker(yellow) has been romoved we have to create a new purple marker
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
            selectedNotficationObject = null;
        }
    }

    public void NotificationPanelRemoved(Vector2 minimapLocation) {
        minimapControl.DeleteSpecifiqMarker(minimapLocation);
    }
}
