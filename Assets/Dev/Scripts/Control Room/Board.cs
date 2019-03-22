using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationMenu;
    private NotificationControl notificationControl;

    // POI
    [SerializeField]
    private GameObject POISystem;
    private POIManager POIManager;

    private NotificationContainer nc;
    private int irrelevantNotificationCount;
    public string m_Path = "XML_Files/data-set";
    Dictionary<int, List<Notification>> notificationsPerPOI = new Dictionary<int, List<Notification>>();

    // NEW
    private LinkedList<DLinkedList> notificationlist;
    private int notificationIndexCounter;

    void Start() {
        LoadItems(m_Path);

        //ToDo remove old way of sorting notifications
        FillDictionaryWithNotificationsPerPOI();

        // NEW
        FillAndConnectNotificationsList();
        Test();

        notificationControl = notificationMenu.GetComponent<NotificationControl>();

        POIManager = POISystem.GetComponent<POIManager>();
        // Count -1 as we don't need a POI on the map for irrelevant messages
        POIManager.Setup(notificationsPerPOI.Count - 1);

        //Setup second display for VR camera
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

    void LoadItems(string path) {
        nc = NotificationContainer.Load(path);
    }

    void FillDictionaryWithNotificationsPerPOI() {
        foreach (Notification note in nc.notifications) {

            if (!notificationsPerPOI.ContainsKey(note.POI)){
                notificationsPerPOI.Add(note.POI, new List<Notification>());
            }
          
            notificationsPerPOI[note.POI].Add(note);
        }
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

    public void ShowNotification() {

        //ToDo think of a good structure to keep track of what messages need to be displayed. 

        Debug.Log("Found " + notificationlist.Count() + " notifications");

        Notification notificationItem = notificationlist.First.Value.GetData();

        foreach(DLinkedList item in notificationlist)
        {
            if (!item.GetData().WaitingForPost)
            {
                notificationItem = item.GetData();
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
            else
            {
                Debug.Log("Found a message thats waitig for a post");
            }
        }

        if (notificationItem != null) {
            SetNotificationPlatformLogo(notificationItem);

            notificationControl.CreateMessagePanel(notificationItem);
        }
        else
        {
            Debug.Log("List is empty");
        }
    }

    private void SetNotificationPlatformLogo(Notification notification) {
        notification.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + notification.Platform);
        if (notification.Image != null) {
            notification.Img = Resources.Load<Sprite>("Images/" + notification.Image);
        }
    }




    }
