using System;
using System.Collections.Generic;
using UnityEngine;

public class POIManager : MonoBehaviour {

    private int currentPOI;

    [SerializeField]
    private GameObject suspect;

    private List<GameObject> POIs = new List<GameObject>();

    public void Setup(int amountOfPOI) {
        currentPOI = 1;
        FillPOIList(amountOfPOI);
    }

    public int GetCurrentPOI() {
        return currentPOI;
    }

    public Transform GetPOITransformData() {
        return POIs[currentPOI - 1].transform;
    }

    public void POIReached() {
        if (currentPOI < POIs.Count) {
            currentPOI++;
            EnableCollider(POIs[currentPOI - 1]);
        } else {
            suspect.GetComponentInChildren<SphereCollider>().enabled = true;
        }
    }

    private void FillPOIList(int amount) {
        for (int i = 0; i < amount; i++) {

            POIs.Add(GetPOIChild(i));
            if (i + 1 != currentPOI) {
                EnableCollider(POIs[i], false);
            }
        }
    }

    // Retrieve POI's from map
    private GameObject GetPOIChild(int index) {
    
        try {
            GameObject POI = gameObject.transform.GetChild(index).gameObject;
            return POI;
        } catch {
            throw new Exception("Unable to find POI on map, make sure POISystem has all the necessary POI's as children");
        }        
    }

    private void EnableCollider(GameObject poiObject, bool value = true) {
        poiObject.GetComponent<SphereCollider>().enabled = value;
    }

}
