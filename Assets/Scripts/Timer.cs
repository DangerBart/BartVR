using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject panelToShowNotifications;
    public float intervalRelevantMessages = 2f;
    public float intervalIrrelevantMessages = 2f;
    private float relevantActionTime = 0.0f;
    private float irrelevantActionTime = 0.0f;

    // Social media creator
    public GameObject socialMediaPrefab;
    private Board board;

    void Start () {
        board = GetComponent<Board>();
	} 

    void Update()
    {
        // Relevant notifications
        if (Time.time > relevantActionTime)
        {
            relevantActionTime += intervalRelevantMessages;
            board.LoadRandomRelevantNotification();
        }

        // irrelevant notifications
        if (Time.time > irrelevantActionTime)
        {
            irrelevantActionTime += intervalIrrelevantMessages;
            board.LoadRandomIrrelevantNotification();
        }

    }

}
