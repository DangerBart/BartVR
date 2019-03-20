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
                    if (notificationsArray[i].FindAndInsertByNotificationId(notif))
                        break;
                }
            }
        }
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




    }
