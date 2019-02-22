using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSync : MonoBehaviour {
    //References
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private GameObject plane;
    [SerializeField]
    private GameObject cameraRig;
    //Variables for storing values during calculations
    private Vector2 mapSize;
    private Vector2 planeSize;
    private float xScale;
    private float yScale;
    //Offset for more precise location to give to the icon
    [SerializeField]
    private float offsetx = 1.7f;
    [SerializeField]
    private float offsety = 2f;

    // Use this for initialization
    void Start () {
        mapSize = getSize(map);
        planeSize = getSize(plane);
    }

    // Update is called once per frame
    void Update () {
        xScale = scale(planeSize.x, mapSize.x, offsetx);
        yScale = scale(planeSize.y, mapSize.y, offsety);
        //Set icon to location of the cameraRig
        GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (cameraRig.transform.position.x * xScale),
                                                                                 -1 * (cameraRig.transform.position.z * yScale));
    }

    Vector2 getSize(GameObject go) {
        return go.GetComponent<RectTransform>().sizeDelta;
    }

    float scale(float planeSize, float mapSize, float offset) {
        return planeSize / mapSize + offset;
    }
}
