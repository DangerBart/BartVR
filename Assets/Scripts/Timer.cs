using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject panelToShowNotifications;
    public float periodInSeconds = 10f;
    private float nextActionTime = 0.0f;

    // Social media creator
    public GameObject socialMediaPrefab;
    GameObject socialMediaPrefabClone;


    // Test
    private int counter = 0;

    // Use this for initialization
    void Start () {
        socialMediaPrefabClone = Instantiate(socialMediaPrefab);
	}

   

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += periodInSeconds;

            // execute block of code here
            counter++;
            Debug.Log("Get messages to show on screen: " + counter);

            //Turned off for now
            //CreateMessage("Hello word", Color.blue);
        }
    }

    public void CreateMessage(string text, Color color)
    {
        GameObject newCanvas = new GameObject("Canvas");
        Canvas c = newCanvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        newCanvas.AddComponent<CanvasScaler>();
        newCanvas.AddComponent<GraphicRaycaster>();
        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Image i = panel.AddComponent<Image>();
        i.color = Color.red;
        panel.transform.SetParent(panelToShowNotifications.transform, false);

    }

    public void AddMessageToPanel(){

    }

}
