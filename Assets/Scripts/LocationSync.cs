using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSync : MonoBehaviour {

	public GameObject map;
	public GameObject plane;
	public GameObject officerInVR;
	private Vector2 mapSize;
	private Vector2 planeSize;
	private float xScale;
	private float yScale;
	private Vector2 officerOnMap;
	private float offsetScale = 0.7f;

	private void Start() {
		UpdateMapSizeAndScale();
		Debug.Log("xScale :" + xScale);
		Debug.Log("yScale :" + yScale);
	}

	void Update () {
		ScaleOfficerOnMap();
	}
	private void UpdateMapSizeAndScale() {
		mapSize = map.GetComponent<RectTransform>().sizeDelta;
		planeSize = plane.GetComponent<RectTransform>().sizeDelta;
		xScale = planeSize.x / mapSize.x + 1 + offsetScale;
		yScale = planeSize.y / mapSize.y + 1 + offsetScale;
	}
	private void ScaleOfficerOnMap() {
		this.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (officerInVR.transform.position.x * xScale), -1 *(officerInVR.transform.position.z * yScale));
	}
}
