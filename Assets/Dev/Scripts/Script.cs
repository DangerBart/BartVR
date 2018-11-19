using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		Debug.Log("ENTERING");
	}
	void OnCollisionStay(Collision collision) {
		Debug.Log("STAYING");
	}
	void OnCollisionExit(Collision collision) {
		Debug.Log("EXITING");
	}
}
