using System.Collections.Generic;
using UnityEngine;

public class NotificationPostReactions : MonoBehaviour
{
    [SerializeField]
    private GameObject postableNotificationPanelPrefab;
    [SerializeField]
    private GameObject notificationPanelPrefab;
    [SerializeField]
    private GameObject postableNotificationContainer;
    [SerializeField]
    private GameObject reactionNotificationContainer;

    private List<DoublyLinkedList> PostableNotifications = new List<DoublyLinkedList>();

    void Start()
    {

    }

    public void AddNewPostableNotification(DoublyLinkedList notif)
    {

        DoublyLinkedList lol = notif;
        while (lol != null)
        {
            Debug.Log(lol.GetData().Message + ", waiting: " + lol.GetData().WaitingForPost);
            lol = lol.GetNext();
        }
        CreatePostableNotificationPanel(notif);

    }

    public void ReactionToPostableMessageHasBeenPosted(int id)
    {
        Debug.Log("Recieved news tha Notification with ID " + id + " has been posted" );
        CreateReactionNotificationPanel(PostableNotifications[0].GetData());
        //SetWaitingForPost(id, false);
    }

    public void ShowReactionsOfPostableMessages()
    {

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

    private bool SetWaitingForPost(int id, bool value) {
        //Find Notification
        Notification foundNotif = findById(id);
        if (foundNotif != null) {
            foundNotif.WaitingForPost = value;
            return true;
        }

        return false;
    }

    private Notification findById(int id) {
        foreach (DoublyLinkedList foundNotif in PostableNotifications) {
            DoublyLinkedList next = foundNotif;
            while (next != null) {
                if (next.GetData().Id == id)
                    break;

                next = next.GetNext();
            }

            if (next != null)
                return next.GetData();
        }
        return null;
    }

    // Test
    private void PrintAll()
    {
        foreach (DoublyLinkedList lol in PostableNotifications)
        {
            string oh = lol.GetData().Id.ToString() + "(" + lol.GetData().WaitingForPost + "), ";

            DoublyLinkedList ay = lol.GetNext();
            while (ay != null) {
                oh +=  ay.GetData().Id + "(" + ay.GetData().WaitingForPost + "),";
                ay = ay.GetNext();
            }
            Debug.Log(oh);
        }
    }
}
