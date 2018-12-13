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

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Repeatedly call the addnotification function with an interval
        InvokeRepeating("AddRelevantNotification", 0, intervalRelevantMessages);
        InvokeRepeating("AddIrrelevantNotification", 0, intervalIrrelevantMessages);
    }

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
