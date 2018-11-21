using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject notificationbar;
    private NotificationContainer nc;
    private Notification[][] NotificationArray;
    // Use this for initialization
    void Start()
    {
        string m_Path = "XML_Files/data-set";
        //Debug.Log("m_path" + m_Path);
        LoadItems(m_Path);
        ReturnListInformation(nc);
    }
    void LoadItems(string path)
    {
        nc = NotificationContainer.Load(path);
    }

    void ReturnListInformation(NotificationContainer nc)
    {
        //The MultiLinked List
        List<List<Notification>> MassiveList = new List<List<Notification>>();
        //List needed to count the POI
        List<int> countPOIList = new List<int>();
        //A temp list to fill in with notifications to add to MultiLinked List
        List<Notification> miniList = new List<Notification>();
        //Collect all the POI and stores them in the List
        foreach (Notification notification in nc.notifications)
        {
            if (!countPOIList.Contains(notification.POI))
            {
                countPOIList.Add(notification.POI);
            }
        }
        for(int i =0; i<countPOIList.Count; i++)
        {
            foreach (Notification notification in nc.notifications)
            {
                if(i == notification.POI)
                {
                    miniList.Add(notification);
                }
            }
            MassiveList.Add(miniList);
            Debug.Log("POI: " + i);
            foreach (Notification no in miniList)
            {
                Debug.Log(no.Voornaam);
            }
            miniList.Clear();
        }
    }

}
