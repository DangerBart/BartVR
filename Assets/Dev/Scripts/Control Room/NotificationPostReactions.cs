using System;
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
    private DoublyLinkedList selectedNotification;

    public void AddNewPostableNotification(DoublyLinkedList notif) {
        PostableNotifications.Add(notif);
        CreatePostableNotificationPanel(notif);
    }

    public void SelectNotification(int id) {
        DoublyLinkedList foundNotif = PostableNotifications.Find(nc => nc.GetData().Id == id);
        selectedNotification = foundNotif;

        if (foundNotif == null)
            throw new Exception("Id was not found, make sure to send an ID of a postable notification");

        ResetDisplayedReactions();
    }

    public void ReactionToPostableMessageHasBeenPosted(int id) {
        ResetDisplayedReactions();
    }

    public void EmptyNotificationReactionContainer() {
        foreach (Transform child in reactionNotificationContainer.transform)
            Destroy(child.gameObject);
    }

    public void DeselectAllPostableNotificationsExcept(int id) {
        DoublyLinkedList foundNotif = PostableNotifications.Find(nc => nc.GetData().Id == id);

        foreach (NotificationPanel panel in postableNotificationContainer.GetComponentsInChildren<NotificationPanel>()) {
            if (panel.GetNotification() == foundNotif)
                panel.SetPanelColor(true);
            else
                panel.SetPanelColor(false);
        }
    }

    private void ResetDisplayedReactions() {
        DoublyLinkedList reaction = selectedNotification.GetNext();
        EmptyNotificationReactionContainer();

        while (reaction != null && !reaction.GetData().WaitingForPost) {
            CreateReactionNotificationPanel(reaction.GetData());
            reaction = reaction.GetNext();
        }
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
