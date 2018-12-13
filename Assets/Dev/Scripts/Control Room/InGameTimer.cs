using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimer : MonoBehaviour {
	[Tooltip("Update in game time every X seconds")]
    public float updateTimeEvery;

    private GameObject timestamp;
    private Text currentDate;
    private Text currentTime;
    private int hour;
    private double minute;

	// Use this for initialization
    void Start () {
		timestamp = this.transform.Find("Timestamp").gameObject;
        currentDate = timestamp.transform.Find("Date").GetComponent<Text>();
        currentTime = timestamp.transform.Find("Time").GetComponent<Text>();
        currentDate.text = "Datum: " + System.DateTime.Now.ToString("dd/MM/yyyy");
        LoadTime();
        System.Action updateGameTimeAlias = UpdateGameTime;
        InvokeRepeating(updateGameTimeAlias.Method.Name,0,updateTimeEvery);
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
        ViewGameTime();
    }
	private void ViewGameTime(){
        currentTime.text = string.Format("Tijd: {0}:{1}", hour.ToString("00"), minute.ToString("00"));
    }
}



