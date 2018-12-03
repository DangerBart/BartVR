using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManager : MonoBehaviour {

    private int currentPOI;
    private int amountOfPOI;

    private List<Transform> POIs = new List<Transform>();


    public void Setup(int amountOfPOI)
    {
        currentPOI = 1;
        this.amountOfPOI = amountOfPOI;
        FillPOIList(amountOfPOI);
        PrintAllPOI();
    }

    private void FillPOIList(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            POIs.Add(GetPOIChild(i));
        }

        Debug.Log("Done with filling the POI list");
    }

    private Transform GetPOIChild(int index) {
        return gameObject.transform.GetChild(index);
    }

    // TEST
    public void PrintAllPOI()
    {
        foreach (Transform POI in POIs)
        {
            Debug.Log(POI.transform.position.x);
        }
    }
}
