using System;
using UnityEngine;

public class LocationSync : MonoBehaviour
{
    [Serializable]
    public struct NpcImageOption
    {
        public GameObject npc;
        public GameObject minimapImage;
    }

    // Public viarables
    public GameObject map;
    public GameObject plane;
    public NpcImageOption[] NpcToDisplayOnMinimap;
    /// <summary>
    ///  Needed to look for suspect
    /// </summary>
    /// Temporry
    public GameObject npcContainer;
    public GameObject suspectIcon;

    // Private viarables 
    private Vector2 mapSize;
    private Vector2 planeSize;
    private float xScale;
    private float yScale;
    public float offsetx = 1.7f;
    public float offsety = 2f;
    private Vector2 officerOnMap;
    
    // Private methods
    private void Start() {
        UpdateMapSizeAndScale();
        GetSuspect();
    }

    // Doesnt work yet
    private NpcImageOption GetSuspect()
    {
        NpcImageOption toReturn = new NpcImageOption();

        foreach (GameObject foundNpc in npcContainer.GetComponentsInChildren<GameObject>())
        {
            if (foundNpc.GetComponent<Identification>().role == Roles.Suspect)
            {
                Debug.Log("Found suspect");
                toReturn.npc = foundNpc;
                toReturn.minimapImage = suspectIcon;
                break;
            }    
        }

        return toReturn;
    }

    void Update() {
        ScaleNpcOnMap(NpcToDisplayOnMinimap[0]);
    }

    private void UpdateMapSizeAndScale() {
        mapSize = map.GetComponent<RectTransform>().sizeDelta;
        planeSize = plane.GetComponent<RectTransform>().sizeDelta;
        xScale = planeSize.x / mapSize.x + offsetx;
        yScale = planeSize.y / mapSize.y + offsety;
    }
    private void ScaleNpcOnMap(NpcImageOption NpcOption) {
        NpcOption.minimapImage.GetComponent<RectTransform>().transform.localPosition = new Vector2(-1 * (NpcOption.npc.transform.position.x * xScale),
                                                                                 -1 * (NpcOption.npc.transform.position.z * yScale));
    }
}
