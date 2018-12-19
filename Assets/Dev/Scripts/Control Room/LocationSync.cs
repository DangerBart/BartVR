using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSync : MonoBehaviour
{

    public GameObject map;
    public GameObject plane;
    public GameObject officerInVR;
    private Vector2 mapSize;
    private Vector2 planeSize;
    private float xScale;
    private float yScale;
    public float offsetx = 1.7f;
    public float offsety = 2f;
    private Vector2 officerOnMap;
    

    private void Start() {
        UpdateMapSizeAndScale();
    }

    void Update() {
        ScaleOfficerOnMap();
    }

    private void UpdateMapSizeAndScale() {
        mapSize = map.GetComponent<RectTransform>().sizeDelta;
        planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        xScale = planeSize.x / mapSize.x + offsetx;
        yScale = planeSize.y / mapSize.y + offsety;
    }
    private void ScaleOfficerOnMap() {
        this.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (officerInVR.transform.position.x * xScale),
                                                                                 -1 * (officerInVR.transform.position.z * yScale));
    }
}
