using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationOverview : MonoBehaviour {

    //The purpose of this class is to display the contents of a MainNotification when recieved.
    #region private viarables
    [SerializeField]
    private static Text keynote;
    private static MainNotification mainNotificationToShow;
    #endregion

    // Use this for initialization
    void Start () {
		
	}

    public static void ShowContentsOfNotificaiton(MainNotification mainNotif) {
        mainNotificationToShow = mainNotif;
        keynote.text = mainNotificationToShow.keyNote;
        Debug.Log("Hewwo");
    }

    public void HideContentsOfNotification() {

    }

}
