using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationMenu;
    private NotificationControl notificationControl;


    private NotificationContainer nc;
    private int amountPOI;
    public string m_Path = "XML_Files/data-set";
    Dictionary<int, List<Notification>> notificationsPerPOI = new Dictionary<int, List<Notification>>();

    void Start()
    {
        LoadItems(m_Path);
        FillDictionaryWithNotificationsPerPOI();
        notificationControl = notificationMenu.GetComponent<NotificationControl>();
    }

    void LoadItems(string path)
    {
        nc = NotificationContainer.Load(path);
    }


    void FillDictionaryWithNotificationsPerPOI(){
        foreach (Notification note in nc.notifications){

            if (!notificationsPerPOI.ContainsKey(note.POI)){
                notificationsPerPOI.Add(note.POI, new List<Notification>());
                amountPOI++;
            }
          
            notificationsPerPOI[note.POI].Add(note);
        }
    } 

    public void LoadRandomNotification(int POI)
    {
        int randomNotificationID = Random.Range(0, notificationsPerPOI[POI].Count);
        Notification not = notificationsPerPOI[POI][randomNotificationID];
        
        not.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + not.Platform);
        if(not.Image != null){
            not.Img = Resources.Load<Sprite>("Images/" + not.Image);
        }
        Debug.Log(not.PlatformLogo);

        notificationControl.CreateMessagePanel(not);
    }

}
