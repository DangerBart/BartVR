using System.Collections.Generic;
using UnityEngine;

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

    void Start() {
        LoadItems(m_Path);
        FillDictionaryWithNotificationsPerPOI();
        notificationControl = notificationMenu.GetComponent<NotificationControl>();

        POIManager = POISystem.GetComponent<POIManager>();
        // Count -1 as we don't need a POI on the map for irrelevant messages
        POIManager.Setup(notificationsPerPOI.Count - 1);
    
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
        // Reset counter if needed
        if (irrelevantNotificationCount >= notificationsPerPOI[0].Count){
            irrelevantNotificationCount = 0;
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
