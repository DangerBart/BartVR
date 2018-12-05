﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour
{
    public GameObject DefaultNotificationPanel;
    public GameObject DefaultBoardPanel;
    public void CreateMessagePanel(Notification notification, GameObject NotPanel) {
        //Make a copy of the hidden panel
        GameObject message = Instantiate(NotPanel) as GameObject;
        message.SetActive(true);
        
        message.GetComponent<NotificationPanel>().SetName(notification.Name);
        message.GetComponent<NotificationPanel>().SetMessage(notification.Message);
        message.GetComponent<NotificationPanel>().SetMediaPlatform(notification.PlatformLogo);
        message.GetComponent<NotificationPanel>().SetImage(notification.Img);

        message.transform.SetParent(NotPanel.transform.parent, false);
    }
    public void ToggleFavoritePanel(GameObject originalPanel, bool isFavorite){
        //Make a copy of the originalpanel
        GameObject message = Instantiate(originalPanel) as GameObject;
        message.SetActive(true);
        //Place it on the boardpanel or the receive notificationpanel
        if(isFavorite){
            message.transform.SetParent(DefaultBoardPanel.transform.parent, false);
        }
        else{
            message.transform.SetParent(DefaultNotificationPanel.transform.parent, false);
        }
    }
}
