using UnityEngine;

public class NPCManager : MonoBehaviour {
    [SerializeField]
    private GameObject CheckpointContainer;
    [SerializeField]
    private string NpcPrefabsPath;

    string officerModelsPath = "Officers";

    // Use this for initialization
    void Awake() {
        NPCMaker npcMaker = GetComponent<NPCMaker>();
        npcMaker.Setup(NpcPrefabsPath, CheckpointContainer);

        // Create suspect
        npcMaker.CreateSuspect();

        for(int i = 0; i < 5; i++)
        {
            npcMaker.CreateOfficer(officerModelsPath);
        }

        // Create all civilians
        for (int i = 0; i < GameManager.amountOfNpcsToSpawn; i++)
            npcMaker.CreateCivilian();
    }
}