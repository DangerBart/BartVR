using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    [Tooltip("Update in game time")]
    public float updateTimeEvery;

    [Tooltip("Time in seconds")]
    [SerializeField]
    private float intervalRelevantMessages = 2f;
    [Tooltip("Time in seconds")]
    [SerializeField]
    private float intervalIrrelevantMessages = 2f;
    private float relevantActionTime = 0.0f;
    private float irrelevantActionTime = 0.0f;

    // Social media creator
    private Board board;
    public GameObject timestamp;
    private Text currentDate;
    private Text currentTime;
    private int hour;
    private double minute;

    void Start () {
        board = GetComponent<Board>();
        currentDate = timestamp.transform.Find("Date").GetComponent<Text>();
        currentTime = timestamp.transform.Find("Time").GetComponent<Text>();
        currentDate.text = "Date: " + System.DateTime.Now.ToString("dd/MM/yyyy");
        LoadTime();
        InvokeRepeating("UpdateGameTime",0,updateTimeEvery);
    } 

    void Update()
    {
        // Relevant notifications
        if (Time.time > relevantActionTime){
            relevantActionTime += intervalRelevantMessages;
            board.LoadRandomRelevantNotification();
        }

        // irrelevant notifications
        if (Time.time > irrelevantActionTime){
            irrelevantActionTime += intervalIrrelevantMessages;
            board.LoadRandomIrrelevantNotification();
        }
    }
    private void LoadTime(){
        var date = System.DateTime.Now;
        hour = date.Hour;
        minute = date.Minute;
    }
    private void UpdateGameTime(){
        minute += 1;
        if(minute >= 60){
            minute = 0;
            hour += 1;
            if(hour >= 24){
                hour = 0;
            }
        }
            
        currentTime.text = string.Format("Time: {0}:{1}", hour.ToString("00"), minute.ToString("00"));
    }
}
