using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    private NotificationContainer nc;
    private int amountPOI;
    public string m_Path = "XML_Files/data-set";
    Dictionary<int, List<Notification>> notificationsPerPOI = new Dictionary<int, List<Notification>>();
    private int switchPOI = 0;

    void Start()
    {
        LoadItems(m_Path);
        FillDictionaryWithNotificationsPerPOI();
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

        //PrintTest();
    } 
    public void LoadRandomNotification(){
        int randomNotificationID = Random.Range(0, notificationsPerPOI[switchPOI].Count);
        Notification not = notificationsPerPOI[switchPOI][randomNotificationID];
        
        not.PlatformLogo = Resources.Load<Sprite>("Mediaplatform/" + not.Platform);
        if(not.Image != null){
            not.Img = Resources.Load<Sprite>("Images/" + not.Image);
        }
        Debug.Log(not.PlatformLogo);
    }

    // Test function -> Remove later
    void PrintTest(){
        for (int i = 0; i < amountPOI; i++){
            Debug.Log("POI: " + i + " contains " + notificationsPerPOI[i].Count + " notifications");
        }
    }

}
