using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectHandler : MonoBehaviour {
    public GameObject player;
    public GameObject gameOverCanvas;

    private float diameter;
    private Transform arrestSphere;

    // Use this for initialization
    void Start () {
        arrestSphere = gameObject.transform.GetChild(1);
        diameter = arrestSphere.lossyScale.y / 4;
    }
	
	// Update is called once per frame
	void Update () {
        //If player is within suspect's arresting radius
		if(player.transform.position.x >= this.transform.position.x || player.transform.position.x <= this.transform.position.x ||
           player.transform.position.z >= this.transform.position.z || player.transform.position.z <= this.transform.position.z) {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
	}
}
