using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour
{

    [SerializeField]
    private GameObject messagePanel;

    public void CreateMessagePanel(Notification notification) {
        GameObject message = Instantiate(messagePanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationButton>().SetName(notification.Name);
        message.GetComponent<NotificationButton>().SetMessage(notification.Message);
        message.GetComponent<NotificationButton>().SetMediaPlatform(notification.PlatformLogo);
        message.GetComponent<NotificationButton>().SetImage(notification.Img);

        message.transform.SetParent(messagePanel.transform.parent, false);
    }
}
