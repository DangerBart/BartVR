using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    public GameObject NPC;
    public GameObject CheckpointContainer;

    [Tooltip("Amount of NPC's to spawn")]
    public int amount;

    private NPCBehaviour npcBehaviour;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < amount; i++) {
            CreateNPC(this.gameObject);
        }
    }

    private void CreateNPC(GameObject container) {
        GameObject npc = Instantiate(NPC) as GameObject;
        npcBehaviour = npc.GetComponent<NPCBehaviour>();
        npcBehaviour.checkpointContainer = CheckpointContainer; 
        npc.SetActive(true);
        npc.transform.SetParent(container.transform);
    }
}
