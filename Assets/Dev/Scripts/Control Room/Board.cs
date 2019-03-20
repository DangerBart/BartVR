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
    private DLinkedList[] notificationsArray;

    void Start() {
        LoadItems(m_Path);
        FillDictionaryWithNotificationsPerPOI();

        // NEW
        FillAndConnectNotificationsList();
        Test();
        //DLinkedList node1 = new DLinkedList(1);
        //DLinkedList node3 = node1.InsertNext(3);
        //DLinkedList node2 = node3.InsertPrev(2);
        //DLinkedList node5 = node3.InsertNext(5);
        //DLinkedList node4 = node5.InsertPrev(4);

        //node1.TraverseFront();
        //node5.TraverseBack();


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
        notificationsArray = new DLinkedList[nc.notifications.Count(x => !x.ReactionTo.HasValue)];

        //Needed for optimization
        int difference = nc.notifications.Count - nc.notifications.Count(x => !x.ReactionTo.HasValue);
        int count = 0;

        foreach (Notification notif in nc.notifications)
        {
            //Debug.Log("ID: " + note.Id + ", reaction to: " + note.ReactionTo + ", postable: " + note.Postable);
            if (!notif.ReactionTo.HasValue) {
                notificationsArray[count] = new DLinkedList(notif);
                count++;
            } else {
                // Notification is a reaction and needs to be connected
                int startLookingAt = (int)notif.ReactionTo - difference;
                if (startLookingAt < 0)
                    startLookingAt = 0;

                for (int i = startLookingAt; i < count; i++)
                {
                    bool found = notificationsArray[i].FindAndInsertByNotificationId(notif);

                    if (found)
                    {
                        Debug.Log("Notification was found");
                        break;
                    }

                }
               

                    //if (notificationsArray[i].Id == notif.ReactionTo)
                    //{
                    //    notificationsArray[i].Next = notif;
                    //    notif.Previous = notificationsArray[i];
                    //} else if (notificationsArray[i].Next != null) {

                //    Notification currentNotif = notificationsArray[i].Next;

                //    while ((currentNotif != null) && (currentNotif.Id != notif.ReactionTo))
                //        currentNotif = currentNotif.Next;

                //    if (currentNotif != null)
                //    {
                //        Debug.Log("Found match in loop: " + currentNotif.Id);
                //        Debug.Log("So notif " + notif.Id + "(" + notif.ReactionTo + ") is attached to " + currentNotif.Id + " next is currently: " + currentNotif.Next);
                //        Debug.Log("NEXT OF NOTIF " + notif.Next);
                //        //This makes is crash
                //        currentNotif.Next = notif;   
                //        notif.Previous = currentNotif;

                //    }

                //}

            }
        }
        //Debug.Log("Count at end: " + count);
    }
    

    private void Test()
    {
        Debug.Log("=======================");
        foreach (DLinkedList notif in notificationsArray)
        {
            notif.TraverseFront();
        }
    }


    public void LoadRandomRelevantNotification() {

        int currentPOI = POIManager.GetCurrentPOI();

        if (notificationsPerPOI[currentPOI].Count != 0) {
            int randomNotificationID = Random.Range(0, notificationsPerPOI[currentPOI].Count);
            Notification notification = notificationsPerPOI[currentPOI][randomNotificationID];

            // Making sure relevant notifications are not displayed twice
            notificationsPerPOI[currentPOI].RemoveAt(randomNotificationID);

            notification.POILocation = POIManager.GetPOILocation();

            SetNotificationPlatformLogo(notification);

            notificationControl.CreateMessagePanel(notification);
        }
    }

    public void LoadRandomIrrelevantNotification() {
        // Doesn't work ATM
        
        // Reset counter if needed
        if (irrelevantNotificationCount >= notificationsPerPOI[0].Count){
            //irrelevantNotificationCount = 0;
        }

        Notification notification = notificationsPerPOI[0][irrelevantNotificationCount];

        SetNotificationPlatformLogo(notification);

        irrelevantNotificationCount++;
        notificationControl.CreateMessagePanel(notification);
    }

    private void SetNotificationPlatformLogo(Notification notification) {
        notification.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + notification.Platform);
        if (notification.Image != null) {
            notification.Img = Resources.Load<Sprite>("Images/" + notification.Image);
        }
    }


    class DLinkedList

    {
        private Notification data;
        private DLinkedList next;
        private DLinkedList prev;

        public DLinkedList()

        {
            data = new Notification();
            next = null;
            prev = null;
        }



        public DLinkedList(Notification value)
        {
            data = value;
            next = null;
            prev = null;
        }

        public DLinkedList InsertNext(Notification notif)
        {

            DLinkedList node = new DLinkedList(notif);
            if (this.next == null)
            {
                // Easy to handle
                node.prev = this;
                node.next = null; // already set in constructor
                this.next = node;

            }else{
                // Insert in the middle
                DLinkedList temp = this.next;
                node.prev = this;
                node.next = temp;
                this.next = node;
                temp.prev = node;
                // temp.next does not have to be changed

            }
            return node;
        }

        public bool FindAndInsertByNotificationId(Notification notif)
        {
            return FindAndInsertByNotificationId(this, notif);
        }

        public bool FindAndInsertByNotificationId(DLinkedList node, Notification notif)
        {
            if (node == null)
                node = this;

            Debug.Log("Traversing in Forward Direction");

            while ((node != null) && (node.data.Id != notif.ReactionTo))
            {
                Debug.Log(node.data.Id);
                node = node.next;
            }
            if (node != null)
            {
                Debug.Log("Found Notif");
                node.InsertNext(notif);
                return true;
            }

            return false;
        }


        public DLinkedList InsertPrev(Notification notif)
        {
            DLinkedList node = new DLinkedList(notif);
            if (this.prev == null)  
            {
                node.prev = null; // already set on constructor
                node.next = this;
                this.prev = node;
            }else{
                // Insert in the middle
                DLinkedList temp = this.prev;
                node.prev = temp;
                node.next = this;
                this.prev = node;
                temp.next = node;
                // temp.prev does not have to be changed

            }
            return node;

        }



        public void TraverseFront()
        {
            TraverseFront(this);
        }



        public void TraverseFront(DLinkedList node)
        {
            if (node == null)
                node = this;

            //Debug.Log("Traversing in Forward Direction for notification: " + node.data.Id);

            string toPrint = "";

            while (node != null)
            {
                toPrint += node.data.Id + ", ";
                node = node.next;
            }
            Debug.Log(toPrint);
        }


        public void TraverseBack()
        {
            TraverseBack(this);
        }



        public void TraverseBack(DLinkedList node)
        {

            if (node == null)
                node = this;

            Debug.Log("Traversing in Backward Direction");

            while (node != null)
            {
                Debug.Log(node.data);
                node = node.prev;

            }
        }
    }

    }
