using System;
using UnityEngine;

public class Officer : MonoBehaviour {
    private GameObject npcContainer;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("NPCContainer") == null)
            throw new System.Exception("No NPCContainer found, make sure the name matches in casing");
        npcContainer = GameObject.Find("NPCContainer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Search(Identification wanted) {
        foreach(Identification idToCheck in npcContainer.GetComponentsInChildren<Identification>()) {
            if (idToCheck.gender.Equals(wanted.gender)) {
                // CHECK HOW MANY VALUES OF WANTED ARE SET AND CROSS REFERENCE ID'S ON SET VALUES
            }
        }
    }

    private bool CheckId(Identification origin, Identification toCheck) {
        return false;
    }
}
