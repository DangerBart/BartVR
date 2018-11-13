using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board :  MonoBehaviour {

    public GameObject notificationbar;

    public void createNotification() {
        Notification notification = new Notification(notificationbar, "SomeMessage", true);
    }
}
