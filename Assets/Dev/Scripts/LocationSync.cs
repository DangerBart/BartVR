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
    

    private void Start() {
        UpdateMapSizeAndScale();
    }

    void Update() {
        ScaleOfficerOnMap();
    }


    private void UpdateMapSizeAndScale() {
        float offset = 2f;

        mapSize = map.GetComponent<RectTransform>().sizeDelta;
        planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        xScale = planeSize.x / mapSize.x + offset;
        yScale = planeSize.y / mapSize.y + offset;
    }
    private void ScaleOfficerOnMap() {
        this.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (officerInVR.transform.position.x * xScale),
                                                                                 -1 * (officerInVR.transform.position.z * yScale));
    }
}
