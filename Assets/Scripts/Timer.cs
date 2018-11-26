using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject panelToShowNotifications;
    public float periodInSeconds = 2f;
    private float nextActionTime = 0.0f;

    // Social media creator
    public GameObject socialMediaPrefab;
    private Board board;


    // Test
    private int counter = 0;

    // Use this for initialization
    void Start () {
        board = GetComponent<Board>();
	} 

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += periodInSeconds;

            // execute block of code here
            counter++;
            board.LoadRandomNotification(0);
        }
    }

}
