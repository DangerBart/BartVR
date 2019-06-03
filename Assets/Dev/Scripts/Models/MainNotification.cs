using System;
using System.Collections.Generic;
using UnityEngine;

public class MainNotification : MonoBehaviour {

    public string keyNote;
    public DateTime timeLatestNotification;
    public Vector2 MinimapLocation;
    public List<Notification> notifications = new List<Notification>();
}
