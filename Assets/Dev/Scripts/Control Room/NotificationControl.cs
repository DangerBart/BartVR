using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private MinimapControl minimapControl;

    // Panels
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

    public void CreateRelevantMessagePanel(DoublyLinkedList notification) {
        //Make a copy of the hidden panel
        //GameObject message = Instantiate(relevantNotificationPanel) as GameObject;
        //message.SetActive(true);

        //message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Relevant);

        //message.transform.SetParent(relevantNotificationPanel.transform.parent, false);

        //minimapControl.SetNotificationMinimapLocation(notification.GetData());
        //minimapControl.CreateNewMarker(notification.GetData().MinimapLocation, true);
        minimapControl.InitiateNotificationOnMinimap(notification.GetData());
    }

    public void CreatePostableMessagePanel(DoublyLinkedList notification)
    {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(postableNotificationPanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Postable);

        message.transform.SetParent(postableNotificationPanel.transform.parent, false);

        minimapControl.SetNotificationMinimapLocation(notification.GetData());
    }

    public void ToggleFavoritePanel(GameObject originalPanel, DoublyLinkedList notification) {
        ////Make a copy of the originalpanel
        //GameObject message = Instantiate(originalPanel) as GameObject;
        //message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Relevant);
        //message.SetActive(true);

        //if(notification.GetData().IsFavorite) {
        //    // Place on favorite board
        //    message.transform.SetParent(favoritePanel.transform.parent, false);

        //    // Create marker when notfication is not selected
        //    if (!notification.GetData().IsSelected)
        //        minimapControl.CreateNewMarker(notification.GetData().MinimapLocation);
        //}
        //else {
        //    // return to original place, either relevant or irrelevant tab
        //    message.transform.SetParent(relevantNotificationPanel.transform.parent, false);
        //    minimapControl.DeleteSpecifiqMarker(notification.GetData().MinimapLocation);
        //}

        //// Making sure selectedNotificationObject is updated
        //if (notification.GetData().IsSelected)
            //selectedNotficationObject = message;
    }

    public void NotificationSelected(GameObject notificationObject) {
        //NotificationPanel notificationPanel = notificationObject.GetComponent<NotificationPanel>();

        //if (selectedNotficationObject != null) {
        //    NotificationPanel selectedNotificationPanel = selectedNotficationObject.GetComponent<NotificationPanel>();

        //    selectedNotificationPanel.TogglePanelColor();
        //    // Remove previous selected marker
        //    minimapControl.DeleteSpecifiqMarker(selectedNotificationPanel.GetMinimapLocation());

        //    // As the selected marker(yellow) has been romoved we have to create a new purple marker
        //    if (selectedNotificationPanel.IsFavorite())
        //        minimapControl.InitiateNotificationOnMinimap(selectedNotificationPanel.GetMinimapLocation());
        //}

        //if (notificationPanel.IsFavorite())
        //    minimapControl.DeleteSpecifiqMarker(notificationPanel.GetMinimapLocation());
          
        //if (selectedNotficationObject != notificationObject) {
        //    // Currently selected and previous panel are not the same
        //    notificationPanel.TogglePanelColor();
        //    selectedNotficationObject = notificationObject;

        //    // Place new minimap marker
        //    minimapControl.CreateNewMarker(notificationPanel.GetMinimapLocation(), true);
        //} else
            //selectedNotficationObject = null;
    }

    public void NotificationPanelRemoved(Vector2 minimapLocation) {
        //minimapControl.DeleteSpecifiqMarker(minimapLocation);
    }
}
