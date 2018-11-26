using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour
{

    [SerializeField]
    private GameObject MessagePanel;

    void Start()
    {
        for (int i = 0; i < 1; i++){
            CreateMessagePanel(i);
        }
    }

    void CreateMessagePanel(int number){
        GameObject message = Instantiate(MessagePanel) as GameObject;
        message.SetActive(true);

        message.GetComponent<NotificationButton>().SetName("User " + number);

        message.transform.SetParent(MessagePanel.transform.parent, false);
    }
}
