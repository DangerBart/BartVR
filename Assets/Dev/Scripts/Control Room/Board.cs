using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    private NotificationControl notificationControl;

    private NotificationContainer nc;
    public string m_Path = "XML_Files/data-set";

    // NEW
    private LinkedList<DLinkedList> notificationlist;

    void Start() {
        LoadItems(m_Path);

        // NEW
        FillAndConnectNotificationsList();

        notificationControl = GetComponent<NotificationControl>();

        //Setup second display for VR camera
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    void LoadItems(string path) {
        nc = NotificationContainer.Load(path);
    }
    
    // NEW
    private void FillAndConnectNotificationsList()
    {
        notificationlist = new LinkedList<DLinkedList>();

        foreach (Notification notif in nc.notifications)
        {
            if (!notif.ReactionTo.HasValue)
                notificationlist.AddLast(new DLinkedList(notif));
            else
            {
                // Notification is a reaction and needs to be connected
                foreach (DLinkedList item in notificationlist)
                {
                    if (item.FindAndInsertByNotificationId(notif))
                        break;
                }
            }
        }
    }


    private void Test()
    {
        Debug.Log("=========== 2 ===========");
        foreach (DLinkedList notif in notificationlist)
        {
            notif.TraverseFront();
        }
    }

    public void SetNotificationWaitingForPost(bool value, int id)
    {
        Debug.Log("Owo");
        DLinkedList foundNotif = notificationlist.FirstOrDefault(nc => nc.GetData().Id == id);
        Debug.Log("FoundNotif message: " + foundNotif.GetData().Message);
        if (foundNotif != null)
            foundNotif.GetData().WaitingForPost = value;
    }

    public void ShowNotification() {
        Debug.Log("Found " + notificationlist.Count() + " notifications");

        DLinkedList notificationItem = notificationlist.First();

        foreach(DLinkedList item in notificationlist)
        {
            if (!item.GetData().WaitingForPost)
            {
                notificationItem = item;
                if (item.HasNext())
                {
                    DLinkedList temporary = item.GetNext();
                    if (item.GetData().Postable)
                        temporary.GetData().WaitingForPost = true;

                    notificationlist.AddFirst(temporary);
                }
                notificationlist.Remove(item);
                break;
            }
        }

        if (notificationItem != null) {
            SetNotificationPlatformLogo(notificationItem.GetData());

            if(notificationItem.GetData().Postable)
                notificationControl.CreatePostableMessagePanel(notificationItem);
            else
                notificationControl.CreateRelevantMessagePanel(notificationItem);
        } else
            Debug.Log("List is empty");
    }

    private void SetNotificationPlatformLogo(Notification notification) {
        notification.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + notification.Platform);
        if (notification.Image != null) {
            notification.Img = Resources.Load<Sprite>("Images/" + notification.Image);
        }
    }
}
