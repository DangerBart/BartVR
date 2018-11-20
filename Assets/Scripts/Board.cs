using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board :  MonoBehaviour {

    public GameObject notificationbar;
    public string XmlPath = "XML_Files/data-set";

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
        foreach (Notification notification in nc.notifications)
        {
            Debug.Log(notification.Voornaam);
        }
    }
}
