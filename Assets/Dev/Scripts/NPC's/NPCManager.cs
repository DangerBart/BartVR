using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    public string NPCPath = "NPC";
    private Object[] gameObjects;
    public GameObject CheckpointContainer;

    private int amount;
    private NPCBehaviour npcBehaviour;

    // Use this for initialization
    void Start () {
        gameObjects = Resources.LoadAll(NPCPath, typeof(GameObject));
        amount = Gamemanager.amountOfNpcsToSpawn;
        for (int i = 0; i < amount; i++) {
            CreateNPC(this.gameObject);
        }
    }

    private void CreateNPC(GameObject container) {
        int rndNPC = Random.Range(0, gameObjects.Length);
        GameObject NPC = Instantiate((GameObject)gameObjects[rndNPC]) as GameObject;
        npcBehaviour = NPC.GetComponent<NPCBehaviour>();
        npcBehaviour.checkpointContainer = CheckpointContainer;
        NPC.SetActive(true);
        NPC.transform.SetParent(container.transform);
    }
}
