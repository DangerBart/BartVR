using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Officer : MonoBehaviour {
    private GameObject npcContainer;
    private NPCBehaviour behaviour;
    private Identification lookingFor;
    private Rigidbody rb;
    private GameObject target;

    private static Identification id;

    private float maxDistance = 33f;
    private bool canQuestion;
    private bool hasQuestioned;
    private static bool startSearching;


    private enum Check {
        None,
        Officer,
        Gender,
        TopPiece,
        BottomPiece,
        GenderAndTop,
        GenderAndBottom,
        TopAndBottom,
        Complete
    }

    // Use this for initialization
    void Start() {
        // Get the NPC container to loop through for searching for the suspect
        if (GameObject.Find("NPCContainer") == null)
            throw new Exception("No NPCContainer found, make sure the name matches in casing");
        npcContainer = GameObject.Find("NPCContainer");

        // Initialize the NPCBehaviour for this officer
        behaviour = this.GetComponent<NPCBehaviour>();

        // Setup this GameObject's collider and rigidbody to only interact with other NPCs
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = true;
        this.GetComponent<SphereCollider>().isTrigger = true;

        id = GameObject.Find("EventSystem").GetComponent<Identification>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Once input is received from the control room, start searching for the given Identification
        if (startSearching && !behaviour.relocating) {

            if (target == null) {
                StartCoroutine(Search(id, Roles.Officer, 1.5f, npcContainer, this.gameObject));
            } else if (!target.GetComponent<NPCBehaviour>().inQuestioning) {
                PursueSuspect(target);
            }

            if (behaviour.inQuestioning)
                behaviour.agent.isStopped = true;
            else
                behaviour.agent.isStopped = false;
        } else
            target = null;

        // TEST
        if (Input.GetKeyDown(KeyCode.N))
            behaviour.RelocateToTarget(new Vector3(-30, 0, -4));
        // END OF TEST -------------------------------------------------------------
    }

    /// <summary>
    /// Checks if wanted identification is in the field of vision of the gameObject this function is called from.
    /// </summary>
    public IEnumerator Search(Identification wanted, Roles searcher, float interval, GameObject npcContainer, GameObject self) {
        yield return new WaitForSeconds(interval);
        GameObject npc;

        // loop through every NPC
        foreach (Identification idToCompare in npcContainer.GetComponentsInChildren<Identification>()) {
            Vector3 npcPosition = new Vector3(idToCompare.GetComponent<Transform>().position.x, 1, idToCompare.GetComponent<Transform>().position.z);
            Vector3 ownPosition = new Vector3(self.transform.position.x, 1, self.transform.position.z);


            // Who is searching for the given Identification
            switch (searcher) {
                case Roles.Officer:
                    // Only loop through civilians and suspect
                    if (idToCompare.role != Roles.Officer && wanted != null) {
                        npc = SearchForWanted(npcPosition, ownPosition, wanted, idToCompare, self);
                        if (npc != null)
                            target = npc;
                    }
                    break;

                case Roles.Suspect:
                    GameObject cop;

                    if (idToCompare.role != Roles.Suspect) {
                        cop = SearchForWanted(npcPosition, ownPosition, wanted, idToCompare, self);
                        if (cop != null) {
                            Suspect.MoveAwayFromTarget(cop, self);
                            cop = null;
                        }
                    }
                    break;
            }
        }
    }

    private GameObject SearchForWanted(Vector3 npcPosition, Vector3 ownPosition, Identification wanted, Identification idToCompare, GameObject self) {
        RaycastHit hit;

        // Find out who we need to look for and then check if who we are looking for is in our field of vision
        if (IsEqual(wanted, idToCompare, LookFor(wanted))) {
            lookingFor = wanted;
            if (Physics.Linecast(ownPosition, npcPosition, out hit)) {
                if (IsInFront(idToCompare.gameObject, self, self.GetComponent<Identification>().role)) {
                    switch (wanted.role) {
                        case Roles.Officer:
                            if (hit.collider.tag == "Officer")
                                return hit.collider.gameObject;
                            break;
                        default:
                            // Check if the NPC we hit has the description we are looking for (in case some NPC blocked the linecast), and if the NPC hasn't been questioned already
                            if (IsEqual(hit.collider.GetComponent<Identification>(), wanted, LookFor(wanted))
                                && !hit.collider.GetComponent<NPCBehaviour>().questioned && !hit.collider.GetComponent<NPCBehaviour>().inQuestioning
                                && hit.collider.tag == "NPC")
                                return hit.collider.gameObject;
                            break;
                    }
                }
            }
        }
        return null;
    }


    // Check what values of the given Identification are set, this way the officer knows what to compare the NPC's to
    private static Check LookFor(Identification id) {
        char[] isSet = { '0', '0', '0' };

        if (id.role == Roles.Officer)
            return Check.Officer;

        if (id.gender != Genders.None)
            isSet[0] = '1';
        if (id.topPiece != Colors.None)
            isSet[1] = '1';
        if (id.bottomPiece != Colors.None)
            isSet[2] = '1';


        string s = new string(isSet);

        switch (s) {
            case "001":
                return Check.BottomPiece;
            case "010":
                return Check.TopPiece;
            case "100":
                return Check.Gender;
            case "011":
                return Check.TopAndBottom;
            case "101":
                return Check.GenderAndBottom;
            case "110":
                return Check.GenderAndTop;
            case "111":
                return Check.Complete;
            default:
                return Check.None;
        }
    }

    private bool IsEqual(Identification origin, Identification compare, Check check) {
        switch (check) {
            case Check.Gender:
                if (origin.gender.Equals(compare.gender))
                    return true;
                return false;
            case Check.Officer:
                if (origin.role.Equals(compare.role))
                    return true;
                return false;
            case Check.TopPiece:
                if (origin.topPiece.Equals(compare.topPiece))
                    return true;
                return false;
            case Check.BottomPiece:
                if (origin.bottomPiece.Equals(compare.bottomPiece))
                    return true;
                return false;
            case Check.GenderAndTop:
                if (origin.gender.Equals(compare.gender) && origin.topPiece.Equals(compare.topPiece))
                    return true;
                return false;
            case Check.GenderAndBottom:
                if (origin.gender.Equals(compare.gender) && origin.bottomPiece.Equals(compare.bottomPiece))
                    return true;
                return false;
            case Check.TopAndBottom:
                if (origin.topPiece.Equals(compare.topPiece) && origin.bottomPiece.Equals(compare.bottomPiece))
                    return true;
                return false;
            case Check.Complete:
                if (origin.gender.Equals(compare.gender) && origin.topPiece.Equals(compare.topPiece) && origin.bottomPiece.Equals(compare.bottomPiece))
                    return true;
                return false;
            default:
                return false;
        }
    }

    // Checks if the given GameObject is within a 90 degree field of view in front of the GameObject this function is called from
    private bool IsInFront(GameObject npc, GameObject self, Roles role) {
        if (role == Roles.Suspect)
            maxDistance = 60f;


        Vector3 directionToTarget = self.transform.position - npc.transform.position;
        float angle = Vector3.Angle(self.transform.forward, directionToTarget);

        if ((Mathf.Abs(angle) > 90 || Mathf.Abs(angle) > 270) && Vector3.Distance(self.transform.position, npc.transform.position) < maxDistance)
            return true;
        return false;
    }


    private void PursueSuspect(GameObject target) {
        if (this.GetComponent<NavMeshAgent>() != null)
            this.GetComponent<NavMeshAgent>().speed = 2.5f;
        canQuestion = true;

        behaviour.MoveToTarget(target);
    }

    // Question the suspect by facing the target and holding for 'time' amount of seconds. If target is the suspect, arrest them
    private IEnumerator Questioning(float time) {
        behaviour.FaceTarget(target.transform);
        yield return new WaitForSeconds(time);
        if (target.GetComponent<Identification>().role == Roles.Suspect)
            GameObject.Find("EventSystem").GetComponent<EventHandler>().ArrestedSuspect();


        hasQuestioned = true;
        behaviour.inQuestioning = false;
        target.GetComponent<Collider>().GetComponent<NPCBehaviour>().inQuestioning = false;
        target = null;
        behaviour.RelocateToTarget(this.transform.position);
        this.GetComponent<SphereCollider>().enabled = true;
    }

    // Once a collision is detected with an NPC check if they match the target and if so, question them
    private void OnTriggerEnter(Collider other) {
        if (other != null && startSearching)
            if (other.GetComponent<Collider>().tag == "NPC" && IsEqual(other.GetComponent<Collider>().GetComponent<Identification>(), lookingFor, LookFor(lookingFor))
                && canQuestion && other.GetComponent<NPCBehaviour>().questioned == false && other.gameObject.GetInstanceID() == target.GetInstanceID()) {

                other.GetComponent<Collider>().GetComponent<NPCBehaviour>().inQuestioning = true;

                other.GetComponent<Collider>().GetComponent<NPCBehaviour>().officerQuestioning = this.gameObject;
                behaviour.inQuestioning = true;
                this.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine(Questioning(5f));
            }
    }

    public static void SetId(Identification set) {
        id.gender = set.gender;
        id.topPiece = set.topPiece;
        id.bottomPiece = set.bottomPiece;
        Check isSet = LookFor(id);
        
        // if more than 1 value is set, activate the arrestBubble
        if (isSet != Check.Gender && isSet != Check.TopPiece && isSet != Check.BottomPiece && isSet != Check.None) {
            FindObjectOfType<ArrestHandler>().gameObject.GetComponent<SphereCollider>().enabled = true;
        }

        startSearching = true;
    }
}
