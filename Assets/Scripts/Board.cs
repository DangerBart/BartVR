using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject notificationbar;
    private NotificationContainer nc;
    private int amountPOI;
    Dictionary<int, List<Notification>> notificationsPerPOI = new Dictionary<int, List<Notification>>();

    void Start()
    {
        string m_Path = "XML_Files/data-set";
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

        PrintTest();
    }

    // Test function -> Remove later
    void PrintTest(){
        for (int i = 0; i < amountPOI; i++){
            Debug.Log("POI: " + i + " contains " + notificationsPerPOI[i].Count + " notifications");
        }
    }

}
