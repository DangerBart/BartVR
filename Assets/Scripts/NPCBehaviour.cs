using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {

	public GameObject checkpointContainer;

	private Vector3 spawnLocation;

	// Use this for initialization
	void Start () {
		spawnLocation = checkpointContainer.gameObject.transform.GetChild(Random.Range(0, checkpointContainer.transform.childCount - 1)).position;
		Debug.Log("spawnLocation: " + spawnLocation);
		Debug.Log("childCount: " + checkpointContainer.transform.childCount);

		this.transform.position = spawnLocation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
