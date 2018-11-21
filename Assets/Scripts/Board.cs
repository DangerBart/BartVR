using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject notificationbar;
    public string XmlPath = "XML_Files/data-set";
    //private List<List<Notification>> notifList = new List<List<Notification>>();
    //private Dictionary<int, Notification> NotificationDictionary = new Dictionary<int, Notification>();

    // Use this for initialization
    void Start()
    {
        string m_Path = "XML_Files/data-set";
        //Debug.Log("m_path" + m_Path);
        LoadItems(m_Path);
    }
    void LoadItems(string path)
    {
        NotificationContainer nc = NotificationContainer.Load(path);
        //foreach (Notification notification in nc.notifications) {
        //    List<Notification> tempList = new List<Notification>();
        //    tempList.Add(notification);
        //    //notifList.Insert(notification.POI,notification);
        //    //NotificationDictionary.Add(notification.POI, notification);
        //}
    }
}
