using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationTimer : MonoBehaviour {

    // Value is set in Unity editor
    [Tooltip("Time in seconds")]
    public float intervalTimeMessages = 2f;

    //Enable scene loading check
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ToDO REMOVE THIS!! It's for testing singleplayer only
        //System.Action addIrrelevantNotificationAlias = ShowNotification;
        //InvokeRepeating(addIrrelevantNotificationAlias.Method.Name, 2, intervalTimeMessages);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Get function so we can extract its name dynamically rather than literal string
        System.Action addIrrelevantNotificationAlias = ShowNotification;

        //Repeatedly call the addnotification function with an interval
        InvokeRepeating(addIrrelevantNotificationAlias.Method.Name, 2, intervalTimeMessages);
    }

    //Remove scene from sceneloading check
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void ShowNotification() {
        //Give the sign that a new notification should be posted.
        GetComponent<Board>().ShowNotification();
    }
}
