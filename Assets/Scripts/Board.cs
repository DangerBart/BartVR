using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board :  MonoBehaviour {

    public GameObject notificationbar;

    public void createNotification() {

    }

    public void exampleNotification() {
        Notification example = new Notification(notificationbar);
        example.content = "0w0 what's this? A notifcation for meee??? >w<";
        example.sender = "cringy weeb";
        example.relevance = true;

        example.add();
    }
}
