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
        minimapControl.InitiateNotificationOnMinimap(notification.GetData());
    }

    public void CreatePostableMessagePanel(DoublyLinkedList notification)
    {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(postableNotificationPanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationPanel>().Setup(notification, KindOfNotification.Postable);

        message.transform.SetParent(postableNotificationPanel.transform.parent, false);
    }

}
