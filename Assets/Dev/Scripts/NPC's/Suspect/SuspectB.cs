using UnityEngine;
using UnityEngine.AI;

public class SuspectB : MonoBehaviour {
    private Identification cop;
    private Officer off;
    private GameObject npcContainer;
    private static NavMeshAgent agent;
    private static NPCBehaviour behaviour;
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
            behaviour.RelocateToTarget(FindRandomPointBehindGameObject(20, self));
            running = true;
        }
    }

    private static Vector3 FindRandomPointBehindGameObject(float radius, GameObject origin) {
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        Vector3 random3DPoint = origin.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        Debug.Log("Relocating");
        Debug.DrawLine(origin.transform.position, random3DPoint, Color.yellow, 5f);
        while (!IsBehind(random3DPoint, origin.transform.position)) {
            randomPoint = Random.insideUnitCircle * radius;
            random3DPoint = origin.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        }

        return origin.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
    }

    private static bool IsBehind(Vector3 point, Vector3 self) {
        Vector3 directionToTarget = self - point;
        float angle = Vector3.Angle(self, directionToTarget);

        if (Mathf.Abs(angle) > 0 || Mathf.Abs(angle) > 180)
            return true;
        return false;
    }
}
