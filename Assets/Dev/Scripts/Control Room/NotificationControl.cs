using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private MinimapControl minimapControl;
    private NotificationOverview notificationOverview;
    private NotificationPostReactions notificationPostReactions;

    // Panels
    [SerializeField]
    private GameObject relevantNotificationPanel;
    [SerializeField]
    private GameObject postableNotificationPanel;

    void Start() {
        minimapControl = minimap.GetComponent<MinimapControl>();
        notificationOverview = FindObjectOfType<NotificationOverview>();
        notificationPostReactions = FindObjectOfType<NotificationPostReactions>();
    }

    public void CreateRelevantMessagePanel(DoublyLinkedList notification) {
        minimapControl.InitiateNotificationOnMinimap(notification.GetData());
    }

    public void CreatePostableMessagePanel(DoublyLinkedList notification) {
        //Make a copy of the hidden panel
        //GameObject message = Instantiate(postableNotificationPanel) as GameObject;
        //message.SetActive(true);

        //message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Postable);

        //message.transform.SetParent(postableNotificationPanel.transform.parent, false);
        notificationPostReactions.AddNewPostableNotification(notification);
    }

    // Let all be done through NotifControl
    public void MarkerClicked(GameObject marker) {
        minimapControl.DeselectMarkersExcept(marker.GetComponent<MainNotification>());
        notificationOverview.ShowContentsOfNotificaiton(marker.GetComponent<MainNotification>());
    }

    public void SelecedMarkerMerged(GameObject marker) {
        notificationOverview.ShowContentsOfNotificaiton(marker.GetComponent<MainNotification>());
    }
}
