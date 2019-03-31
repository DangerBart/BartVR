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

    private GameObject gameOverText;
    private GameObject gameOverScreen;

    private static Identification id;

    private bool canQuestion;
    private bool hasQuestioned;
    private static bool startSearching;

    private enum Check {
        None,
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
        this.gameObject.layer = 9;
        this.GetComponent<SphereCollider>().isTrigger = true;

        id = new Identification();

        target = null;
    }

    // Update is called once per frame
    void Update() {
        // Once input is received from the control room, start searching for the given Identification
        if (startSearching && !behaviour.relocating) {
            if (target == null)
                StartCoroutine(Search(id, Roles.Officer, 1.5f));
            else
                PursueSuspect(target);

            if (behaviour.inQuestioning)
                behaviour.agent.isStopped = true;
            else
                behaviour.agent.isStopped = false;
        } else {
            target = null;
        }

        // START OF TEST -----------------------------------------------------------
        Identification test = new Identification();

        test.gender = Genders.Female;
        test.topPiece = Colors.None;
        test.bottomPiece = Colors.None;

        if (Input.GetKeyDown(KeyCode.N))
            behaviour.RelocateToTarget(new Vector3(-30, 0, -4));
        if (Input.GetKeyDown(KeyCode.M))
            SetId(test);
        // END OF TEST -------------------------------------------------------------
    }

    /// <summary>
    /// Checks if wanted identification is in the field of vision of the gameObject this function is called from.
    /// </summary>
    public IEnumerator Search(Identification wanted, Roles searcher, float interval) {
        yield return new WaitForSeconds(interval);
        // loop through every NPC
        foreach (Identification idToCompare in npcContainer.GetComponentsInChildren<Identification>()) {
            // Who is searching for the given Identification
            switch (searcher) {
                case Roles.Officer:
                    // Only loop through civilians and suspect
                    if (idToCompare.role != Roles.Officer) {

                        // Linecasting information
                        Vector3 npcPosition = new Vector3(idToCompare.GetComponent<Transform>().position.x, 1, idToCompare.GetComponent<Transform>().position.z);
                        Vector3 ownPosition = new Vector3(this.transform.position.x, 1, this.transform.position.z);
                        RaycastHit hit;

                        // Find out who we need to look for and then check if who we are looking for is in our field of vision
                        if (IsEqual(wanted, idToCompare, LookFor(wanted))) {
                            lookingFor = wanted;
                            if (Physics.Linecast(ownPosition, npcPosition, out hit)) {
                                if (hit.collider.tag == "NPC" && IsInFront(idToCompare.gameObject) && Vector3.Distance(ownPosition, npcPosition) < 33f) {
                                    // Check if the NPC we hit has the description we are looking for (in case some NPC blocked the linecast), and if the NPC hasn't been questioned already
                                    if (IsEqual(hit.collider.GetComponent<Identification>(), wanted, LookFor(wanted)) && !hit.collider.GetComponent<NPCBehaviour>().questioned) {
                                        target = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Roles.Suspect:
                    // IMPLEMENT SUSPECT BEHAVIOUR ONCE OFFICER IS SPOTTED
                    break;
            }
        }
    }

    /// <summary>
    /// Check what values of the given Identification are set, this way the officer knows what to compare the NPC's to
    /// </summary>
    private Check LookFor(Identification id) {
        char[] isSet = { '0', '0', '0' };

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
                if (origin.Equals(compare))
                    return true;
                return false;
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks if the given GameObject is within a 90 degree field of view in front of the GameObject this function is called from
    /// </summary>
    private bool IsInFront(GameObject npc) {
        Vector3 directionToTarget = transform.position - npc.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (Mathf.Abs(angle) > 90 || Mathf.Abs(angle) > 270)
            return true;
        return false;
    }


    private void PursueSuspect(GameObject target) {
        if (this.GetComponent<NavMeshAgent>() != null)
            this.GetComponent<NavMeshAgent>().speed = 2.5f;
        canQuestion = true;

        behaviour.MoveToTarget(target);
    }

    /// <summary>
    /// Question the suspect by facing the target and holding for 'time' amount of seconds. If target is the suspect, arrest them
    /// </summary>
    private IEnumerator Questioning(float time) {
        behaviour.FaceTarget(target.transform);
        yield return new WaitForSeconds(time);
        if (target.GetComponent<Identification>().role == Roles.Suspect)
            EventHandler.End();


        hasQuestioned = true;
        behaviour.inQuestioning = false;
        target.GetComponent<Collider>().GetComponent<NPCBehaviour>().inQuestioning = false;
        target = null;
        this.GetComponent<SphereCollider>().enabled = true;
    }

    /// <summary>
    /// Once a collision is detected with an NPC check if they match the target and if so, question them
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if (other != null)
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

        startSearching = true;
    }
}
