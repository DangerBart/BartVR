using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour
{

    [SerializeField]
    private GameObject MessagePanel;

    void Start()
    {
   
    }

    public void CreateMessagePanel(Notification not)
    {
        GameObject message = Instantiate(MessagePanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationButton>().SetName(not.Name);
        message.GetComponent<NotificationButton>().SetMessage(not.Message);
        message.GetComponent<NotificationButton>().SetMediaPlatform(not.PlatformLogo);


        message.transform.SetParent(MessagePanel.transform.parent, false);
    }
}
