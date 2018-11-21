using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject notificationbar;
    private NotificationContainer nc;
    private int AllPOI;
    private List<List<Notification>> MassiveList = new List<List<Notification>>();
    // Use this for initialization
    void Start()
    {
        string m_Path = "XML_Files/data-set";
        //Debug.Log("m_path" + m_Path);
        LoadItems(m_Path);
        AllPOI = CountAllDifferentPOI(nc);
        FillMultiLinkedList(nc, AllPOI);
    }
    void LoadItems(string path)
    {
        nc = NotificationContainer.Load(path);
    }

    int CountAllDifferentPOI(NotificationContainer nc)
    {
        List<int> countPOIList = new List<int>();

        foreach (Notification notification in nc.notifications)
        {
            if (!countPOIList.Contains(notification.POI))
            {
                countPOIList.Add(notification.POI);
            }
        }
        return countPOIList.Count;
    }
    void FillMultiLinkedList(NotificationContainer nc, int POI)
    {
        List<Notification> miniList = new List<Notification>();
        for (int i = 0; i < POI; i++)
        {
            foreach (Notification notification in nc.notifications)
            {
                if (i == notification.POI)
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
