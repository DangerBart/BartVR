using UnityEngine;

public class NotificationPostReactions : MonoBehaviour {
    [SerializeField]
    private GameObject postableNotificationPanelPrefab;
    [SerializeField]
    private GameObject notificationPanelPrefab;
    [SerializeField]
    private GameObject postableNotificationContainer;
    [SerializeField]
    private GameObject reactionNotificationContainer;


    void Start () {
		
	}

    public void AddNewPostableNotification(DoublyLinkedList notif) {

        DoublyLinkedList lol = notif;
        while (lol != null)
        {
            Debug.Log(lol.GetData().Message);
            lol = lol.GetNext();
        }

        CreatePostableNotificationPanel(notif);

    }

    public void ShowReactionsOfPostableMessages() {

    }

    private void CreatePostableNotificationPanel(DoublyLinkedList notif) {
        GameObject message = Instantiate(postableNotificationPanelPrefab) as GameObject;
        message.SetActive(true);
        message.GetComponent<NotificationPanel>().Setup(notif, KindOfNotification.Postable);
        message.transform.SetParent(postableNotificationContainer.transform, false);
    }


    private void CreateReactionNotificationPanel(Notification notif) {
        GameObject message = Instantiate(notificationPanelPrefab) as GameObject;
        message.SetActive(true);
        message.GetComponent<NotificationPanel>().Setup(notif);
        message.transform.SetParent(reactionNotificationContainer.transform, false);
    }
}
