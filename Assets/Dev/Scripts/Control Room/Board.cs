﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField]
    private string m_Path = "XML_Files/data-set";
    [SerializeField]
    private GameObject PostableNotifcationsContentContainer;

    private NotificationContainer nc;
    private NotificationControl notificationControl;
    private string PostableTabDefaultText;
    private LinkedList<DoublyLinkedList> notificationlist;

    void Start() {
        LoadItems(m_Path);
        FillAndConnectNotificationsList();
        notificationControl = GetComponent<NotificationControl>();

        //Setup second display for VR camera
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    public void ShowNotification() {
        DoublyLinkedList notificationItem = notificationlist.First();

        foreach (DoublyLinkedList item in notificationlist) {
            if (!item.GetData().WaitingForPost) {

                notificationItem = item;
                if (item.HasNext()) {
                    DoublyLinkedList temporary = item.GetNext();

                    //Set reaction message on wait mode
                    if (item.GetData().Postable && item.HasNext())
                        item.GetNext().GetData().WaitingForPost = true;

                    notificationlist.AddFirst(temporary);
                }

                notificationlist.Remove(item);
                break;
            }
        }

        //Debug.Log("Message to show: " + notificationItem.GetData().Message + ", waiting on post: " + notificationItem.GetData().WaitingForPost);

        // Tell notificationControl to create a panel for the message
        if (notificationItem != null) {
            SetNotificationPlatformLogo(notificationItem.GetData());
            notificationItem.GetData().PostTime =GameObject.Find("Time").GetComponent<Text>().text;

            if (notificationItem.GetData().Postable)
                notificationControl.CreatePostableMessagePanel(notificationItem);
            else
                notificationControl.CreateRelevantMessagePanel(notificationItem);
        }
    }

    private void FillAndConnectNotificationsList() {
        notificationlist = new LinkedList<DoublyLinkedList>();

        foreach (Notification notif in nc.notifications) {
            if (!notif.ReactionTo.HasValue)
                notificationlist.AddLast(new DoublyLinkedList(notif));
            else {
                // Notification is a reaction and needs to be connected
                foreach (DoublyLinkedList item in notificationlist)
                    if (item.FindAndInsertByNotificationId(notif))
                        break;
            }
        }
    }

    public void SetNotificationWaitingForPost(bool value, int id) {
        DoublyLinkedList foundNotif = notificationlist.FirstOrDefault(nc => nc.GetData().Id == id);

        // If the notification was found
        if (foundNotif != null)
            foundNotif.GetData().WaitingForPost = value;
    }

    // Make sure the image is loaded in
    private void SetNotificationPlatformLogo(Notification notification) {
        notification.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + notification.Platform);
        if (notification.Image != null) {
            notification.Img = Resources.Load<Sprite>("Images/" + notification.Image);
        }
    }

    void LoadItems(string path) {
        nc = NotificationContainer.Load(path);
    }
}
