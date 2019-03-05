using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject CheckpointContainer;
    public string NpcPrefabsPath;

    // Use this for initialization
    void Start()
    {
        NPCMaker npcMaker = GetComponent<NPCMaker>();
        npcMaker.Setup(NpcPrefabsPath, CheckpointContainer);

        // Create suspect
        npcMaker.CreateSuspect();

        // Create all civilians
        for (int i = 0; i < Gamemanager.amountOfNpcsToSpawn; i++)
            npcMaker.CreateCivilian();
    }
}