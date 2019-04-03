using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField]
    private string m_Path = "XML_Files/data-set";
    [SerializeField]
    private GameObject PostableNotifcationsContentContainer;
    [SerializeField]
    private Text PostableTabText;

    private NotificationContainer nc;
    private NotificationControl notificationControl;
    private string PostableTabDefaultText;
    private LinkedList<DLinkedList> notificationlist;

    public object MyProperty { get; set; }

    void Start() {
        LoadItems(m_Path);
        FillAndConnectNotificationsList();
        notificationControl = GetComponent<NotificationControl>();

        PostableTabDefaultText = PostableTabText.text;

        //Setup second display for VR camera
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    public void ShowNotification() {
        DLinkedList notificationItem = notificationlist.First();

        foreach (DLinkedList item in notificationlist) {
            if (!item.GetData().WaitingForPost) {

                notificationItem = item;
                if (item.HasNext())
                {
                    DLinkedList temporary = item.GetNext();

                    //Set reaction message to wait mode
                    if (item.GetData().Postable)
                        temporary.GetData().WaitingForPost = true;

                    notificationlist.AddFirst(temporary);
                }
                notificationlist.Remove(item);
                break;
            }
        }

        // Tell notificationControl to create a panel for the message
        if (notificationItem != null) {
            SetNotificationPlatformLogo(notificationItem.GetData());

            if (notificationItem.GetData().Postable)
                notificationControl.CreatePostableMessagePanel(notificationItem);
            else
                notificationControl.CreateRelevantMessagePanel(notificationItem);

            // Change tab text
            ChangeTextBasedOnCount(PostableTabText, PostableTabDefaultText, PostableNotifcationsContentContainer.transform.childCount - 1);
        }
    }

    private void ChangeTextBasedOnCount(Text textToChange, string defaultText, int count)
    {
        string newText = defaultText;

        if (count > 0)
            newText = newText + " (" + count + ")";

        textToChange.text = newText;
    }

    private void FillAndConnectNotificationsList() {
        notificationlist = new LinkedList<DLinkedList>();

        foreach (Notification notif in nc.notifications)
        {
            if (!notif.ReactionTo.HasValue)
                notificationlist.AddLast(new DLinkedList(notif));
            else {
                // Notification is a reaction and needs to be connected
                foreach (DLinkedList item in notificationlist)
                {
                    if (item.FindAndInsertByNotificationId(notif))
                        break;
                }
            }
        }
    }

    public void SetNotificationWaitingForPost(bool value, int id) {
        DLinkedList foundNotif = notificationlist.FirstOrDefault(nc => nc.GetData().Id == id);

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
