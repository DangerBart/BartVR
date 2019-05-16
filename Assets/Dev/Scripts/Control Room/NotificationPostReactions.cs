using System.Collections.Generic;
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

    private List<DoublyLinkedList> PostableNotifications = new List<DoublyLinkedList>();

    void Start () {
		
	}

    public void AddNewPostableNotification(DoublyLinkedList notif) {

        DoublyLinkedList lol = notif;
        while (lol != null)
        {
            Debug.Log(lol.GetData().Message + ", waiting: " + lol.GetData().WaitingForPost);
            lol = lol.GetNext();
        }

        CreatePostableNotificationPanel(notif);

    }

    public void ReactionToPostableMessageHasBeenPosted(int id){
        Debug.Log("Notification with id " + id + " has beenb posted");
    }

    public void ShowReactionsOfPostableMessages() {

    }

    private void CreatePostableNotificationPanel(DoublyLinkedList notif) {
        GameObject message = Instantiate(postableNotificationPanelPrefab) as GameObject;
        message.SetActive(true);
        message.GetComponent<NotificationPanel>().Setup(notif, KindOfNotification.Postable);
        message.transform.SetParent(postableNotificationContainer.transform, false);

        PostableNotifications.Add(notif);
    }


    private void CreateReactionNotificationPanel(Notification notif) {
        GameObject message = Instantiate(notificationPanelPrefab) as GameObject;
        message.SetActive(true);
        message.GetComponent<NotificationPanel>().Setup(notif);
        message.transform.SetParent(reactionNotificationContainer.transform, false);
    }
}
