using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectHandler : MonoBehaviour {
    public GameObject player;
    public GameObject gameOverText;

    private float radius;
    private Transform arrestSphere;

    // Use this for initialization
    void Start () {
        arrestSphere = gameObject.transform.GetChild(1);
        radius = arrestSphere.lossyScale.y / 4;
    }
	
	// Update is called once per frame
	void Update () {
        //If player is within suspect's arresting radius
		if((player.transform.position.x >= this.transform.position.x + radius || player.transform.position.x <= this.transform.position.x - radius) &&
           (player.transform.position.z >= this.transform.position.z + radius || player.transform.position.z <= this.transform.position.z - radius)) {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Player x: " + player.transform.position.x);
            Debug.Log("Player z: " + player.transform.position.z);
            Debug.Log("Suspect x: " + this.transform.position.x);
            Debug.Log("Suspect z: " + this.transform.position.z);
        }
	}
}
