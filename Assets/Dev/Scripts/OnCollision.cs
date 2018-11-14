using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		Debug.Log("HEADSET COLLISION");
		if(collision.gameObject.tag == "Hands") {
			Debug.Log("COLLISION WITH HANDS");
		} else {
			SteamVR_Fade.Start(Color.black, 0f);
		}
	}
	void OnCollisionStay(Collision collision) {
		Debug.Log("STILL HEADSET COLLISION");

	}
	void OnCollisionExit(Collision collision) {
		SteamVR_Fade.Start(Color.clear,0f);
		Debug.Log("NO HEADSET COLLISION");
	}
}
