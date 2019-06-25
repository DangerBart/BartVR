using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Character controller: " + this.GetComponent<CharacterController>());
        Debug.Log("Sphere Collider: " + NPCMaker.suspect.GetComponentInChildren<SphereCollider>());
        Physics.IgnoreCollision(this.GetComponent<CharacterController>(), GameObject.Find("ArrestBubble(Clone)").GetComponent<SphereCollider>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collided with: " + collision.collider.name);
    }
}
