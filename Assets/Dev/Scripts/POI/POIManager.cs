using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManager : MonoBehaviour {

    private int currentPOI;
    private int amountOfPOI;

    private List<GameObject> POIs = new List<GameObject>();

    public void Setup(int amountOfPOI) {
        currentPOI = 1;
        this.amountOfPOI = amountOfPOI;
        FillPOIList(amountOfPOI);
        PrintAllPOI();
    }

    public int GetCurrentPOI() {
        return currentPOI;
    }

    public void POIReached() {
        if (currentPOI <= POIs.Count) {
            currentPOI++;
            EnableCollider(POIs[currentPOI - 1]);
        }
        else
        {
            Debug.Log("Last POI reached");
        }

        Debug.Log("Current POI is now: " + currentPOI);
    }

    private void FillPOIList(int amount) {
        for (int i = 0; i < amount; i++) {

            POIs.Add(GetPOIChild(i));
            if (i + 1 != currentPOI) {
                EnableCollider(POIs[i], false);
            }
        }

        Debug.Log("Done with filling the POI list");
    }

    private GameObject GetPOIChild(int index) {
        return gameObject.transform.GetChild(index).gameObject;
    }

    private void EnableCollider(GameObject poiObject, bool value = true)
    {
        poiObject.GetComponent<SphereCollider>().enabled = value;
    }


    // TEST
    public void PrintAllPOI()
    {
        foreach (GameObject POI in POIs)
        {
            Debug.Log(POI.transform.position.x);
        }
    }

}
