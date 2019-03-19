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
            
        }
    }

    private bool CheckId(Identification origin, Identification toCheck) {
        return false;
    }
}
