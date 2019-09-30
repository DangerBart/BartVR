using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationSync : MonoBehaviour
{
    [Serializable]
    public struct NpcImageOption {
        public GameObject npc;
        public GameObject minimapImage;
    }

    // Public viarables
    public GameObject map;
    public GameObject plane;
    public List<NpcImageOption> NpcToDisplayOnMinimap;
    public GameObject npcContainer;
    public GameObject suspectIcon;
    public GameObject officerIcon;

    // Private viarables 
    private Vector2 mapSize;
    private Vector2 planeSize;
    private float xScale;
    private float yScale;
    public float offsetx = 1.7f;
    public float offsety = 2f;
    private Vector2 officerOnMap;
    
    // Public methods
    public Vector2 GetSuspectMinimapLocation()
    {
        // Last item in array is suspect
        return NpcToDisplayOnMinimap[NpcToDisplayOnMinimap.Count-1].minimapImage.GetComponent<RectTransform>().transform.localPosition;
    }

    // Private methods
    private void Start() {
        officerIcon.SetActive(false);
        UpdateMapSizeAndScale();
        ReplaceArrayAndAddNpc(GetSuspect(suspectIcon));
    }

    private void ReplaceArrayAndAddNpc(NpcImageOption toAddNpc) {
        foreach (Transform child in npcContainer.transform)
        {
            GameObject npc = child.gameObject;
            Debug.Log("Foreach loop: " + npc);
            if (npc.tag == "Officer")
            {
                NpcImageOption officer = new NpcImageOption();
                officer.npc = npc;
                GameObject icon = Instantiate(officerIcon);
                icon.transform.SetParent(map.transform);
                officer.minimapImage = icon;
                icon.SetActive(true);
                NpcToDisplayOnMinimap.Add(officer);
            }
        }
        NpcToDisplayOnMinimap.Add(toAddNpc);
    }

    private NpcImageOption GetSuspect(GameObject iconToAttatch) {
        NpcImageOption toReturn = new NpcImageOption();

        foreach (Identification idToCompare in npcContainer.GetComponentsInChildren<Identification>()) {

            if (idToCompare.role == Roles.Suspect) {
                toReturn.npc = idToCompare.gameObject;
                toReturn.minimapImage = iconToAttatch;
                break;
            }
        }
        return toReturn;
    }

    void Update() {
        foreach(NpcImageOption npc in NpcToDisplayOnMinimap)
            ScaleNpcOnMap(npc);
    }

    private void UpdateMapSizeAndScale() {
        mapSize = map.GetComponent<RectTransform>().sizeDelta;
        planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        xScale = (planeSize.x / mapSize.x) + offsetx;
        yScale = (planeSize.y / mapSize.y) + offsety;
    }

    private void ScaleNpcOnMap(NpcImageOption NpcOption) {
        NpcOption.minimapImage.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (NpcOption.npc.transform.position.x * xScale),
                                                                                 -1 * (NpcOption.npc.transform.position.z * yScale));
    }
}
