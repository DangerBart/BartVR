using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationTimer : MonoBehaviour {

    [Tooltip("Time in seconds")]
    public float intervalRelevantMessages = 2f;
    [Tooltip("Time in seconds")]
    public float intervalIrrelevantMessages = 2f;

    //Enable scene loading check
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Get function so we can extract its name dynamically rather than literal string
        System.Action addRelevantNotificationAlias = AddRelevantNotification;
        System.Action addIrrelevantNotificationAlias = AddIrrelevantNotification;
        //Repeatedly call the addnotification function with an interval
        InvokeRepeating(addRelevantNotificationAlias.Method.Name, 0, intervalRelevantMessages);
        InvokeRepeating(addIrrelevantNotificationAlias.Method.Name, 0, intervalIrrelevantMessages);
    }

    //Remove scene from sceneloading check
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void AddRelevantNotification() {
        this.GetComponent<Board>().LoadRandomRelevantNotification();
    }
    void AddIrrelevantNotification() {
        this.GetComponent<Board>().LoadRandomIrrelevantNotification();
    }
}
