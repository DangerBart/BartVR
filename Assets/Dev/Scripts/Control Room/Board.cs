using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationMenu;

    // POI
    [SerializeField]
    private GameObject POISystem;
    private POIManager POIManager;

    private NotificationControl notificationControl;

    private NotificationContainer nc;
    private int irrelevantNotificationCount;
    public string m_Path = "XML_Files/data-set";
    Dictionary<int, List<Notification>> notificationsPerPOI = new Dictionary<int, List<Notification>>();

    void Start() {
        LoadItems(m_Path);
        FillDictionaryWithNotificationsPerPOI();
        notificationControl = notificationMenu.GetComponent<NotificationControl>();

        // POI
        POIManager = POISystem.GetComponent<POIManager>();
        // POI 0 is not a real POI
        POIManager.Setup(notificationsPerPOI.Count - 1); 
    }

    public void PlayerMoved(Transform playerCoordinates)
    {
        Debug.Log("Player moved -> We should check if the player is near the current POI. These are the player cordinates: X " + playerCoordinates.transform.position.x + " Y " + playerCoordinates.transform.position.y);
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
        if (notificationsPerPOI[POIManager.GetCurrentPOI()].Count != 0) {
            int index = POIManager.GetCurrentPOI();
            int randomNotificationID = Random.Range(0, notificationsPerPOI[index].Count);
            Notification notification = notificationsPerPOI[index][randomNotificationID];

            // Making sure relevant notifications are not displayed twice
            notificationsPerPOI[index].RemoveAt(randomNotificationID);

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
