using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private MinimapControl minimapControl;
    private NotificationOverview notificationOverview;
    private NotificationPostReactions notificationPostReactions;

    void Start() {
        minimapControl = minimap.GetComponent<MinimapControl>();
        notificationOverview = FindObjectOfType<NotificationOverview>();
        notificationPostReactions = FindObjectOfType<NotificationPostReactions>();
    }

    public void CreateRelevantMessagePanel(DoublyLinkedList notification) {
        Debug.Log("Recieved message to create notif in notifControl: " + notification.GetData().Message);

        minimapControl.InitiateNotificationOnMinimap(notification.GetData());

        if (notification.GetData().ReactionOfPostableNotif)
        {
            notificationPostReactions.ReactionToPostableMessageHasBeenPosted(notification.GetData().Id);
        }
    }

    public void CreatePostableMessagePanel(DoublyLinkedList notification) {
        if (GameManager.currentMode == PlayingMode.Multiplayer)
            notificationPostReactions.AddNewPostableNotification(notification);
    }

    public void MarkerClicked(GameObject marker) {
        minimapControl.DeselectMarkersExcept(marker.GetComponent<MainNotification>());
        notificationOverview.ShowContentsOfNotificaiton(marker.GetComponent<MainNotification>());
    }

    public void PostedNotificationPanelClicked(int id) {
        notificationPostReactions.DeselectAllPostableNotificationsExcept(id);
        notificationPostReactions.SelectNotification(id);
    }

    public void SelecedMarkerMerged(GameObject marker) {
        notificationOverview.ShowContentsOfNotificaiton(marker.GetComponent<MainNotification>());
    }
}
