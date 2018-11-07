using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSync : MonoBehaviour {

	public GameObject officer;
	public GameObject officerMap;

	private RectTransform officerMapTransform;

	void Update () {
		Vector2 vect = new Vector3(officer.transform.position.x, officer.transform.position.z, 0);
		Debug.Log(officer.transform.position.x);
		Debug.Log(officer.transform.position.z);

		officerMap.GetComponent<RectTransform>().position = vect;
	}
}
