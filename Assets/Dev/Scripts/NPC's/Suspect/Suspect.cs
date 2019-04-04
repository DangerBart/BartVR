using System.Collections.Generic;
using UnityEngine;

public class Suspect : MonoBehaviour {
    private Identification cop;
    private Officer off;
    private GameObject npcContainer;
    private static NPCBehaviour behaviour;
    private static readonly float maxDistance = 75f;

    public static bool running;


    void Start() {
        cop = new Identification();
        off = new Officer();
        npcContainer = GameObject.Find("NPCContainer");
        behaviour = this.GetComponent<NPCBehaviour>();

        cop.role = Roles.Officer;
        cop.gender = Genders.None;
        cop.topPiece = Colors.None;
        cop.bottomPiece = Colors.None;
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(off.Search(cop, this.GetComponent<Identification>().role, 2f, npcContainer, this.gameObject));
    }

    public static void MoveAwayFromTarget(GameObject target, GameObject self) {
        if (!running) {
            List<Transform> validNodes = new List<Transform>();

            // Get all nodes behind the suspect and within a maximum distance
            foreach (Transform node in behaviour.GetNodes())
                if (IsBehind(node.position, self.transform.position) && Vector3.Distance(self.transform.position, node.position) < maxDistance)
                    validNodes.Add(node);

            Transform rand = validNodes[Random.Range(0, validNodes.Count)];
            
            behaviour.RelocateToTarget(rand.position);
            running = true;
        }
    }

    private static bool IsBehind(Vector3 point, Vector3 self) {
        Vector3 directionToTarget = self - point;
        float angle = Vector3.Angle(self, directionToTarget);

        if (Mathf.Abs(angle) > 0 || Mathf.Abs(angle) > 180)
            return true;
        return false;
    }
}
