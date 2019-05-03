using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationOverview : MonoBehaviour {

    //The purpose of this class is to display the contents of a MainNotification when recieved.
    #region private viarables
    [SerializeField]
    private Text keynote;
    [SerializeField]
    private GameObject notificationContainer;
    [SerializeField]
    private GameObject notificationPanelPrefab;

    private MainNotification mainNotificationToShow;
    #endregion

    // Use this for initialization
    void Start () {
		
	}

    public void ShowContentsOfNotificaiton(MainNotification mainNotif) {
        mainNotificationToShow = mainNotif;
        keynote.text = mainNotificationToShow.keyNote;
        Debug.Log("Hewwo");
    }

    private void CreateNotificationPanel(Notification notif) {
        GameObject message = Instantiate(notificationPanelPrefab) as GameObject;
        message.SetActive(true);

        //ToDO Make notificationPanel work in new structure
        //message.GetComponent<NotificationPanel>().Setup(notif, KindOfNotification.Postable);

        message.transform.SetParent(notificationContainer.transform, false);
    }

    public void HideContentsOfNotification() {

    }

    private void EmptyNotificationContainer() {
        foreach (Transform child in notificationContainer.transform)
            GameObject.Destroy(child.gameObject);
    }

}
