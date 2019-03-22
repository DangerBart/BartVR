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

    //TEMPORARY REMOVE ONCE FINISHED
    private Identification id;

    private bool canQuestion;
    private bool hasQuestioned;

    enum Check {
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

        if (GameObject.Find("NPCContainer") == null)
            throw new Exception("No NPCContainer found, make sure the name matches in casing");
        npcContainer = GameObject.Find("NPCContainer");

        behaviour = this.GetComponent<NPCBehaviour>();
        rb = this.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.detectCollisions = true;
        // Set this gameObject to the 'Officer' layer for collision ignoring
        this.gameObject.layer = 9;
        this.GetComponent<SphereCollider>().isTrigger = true;

        id = new Identification();
        id.gender = Genders.Male;
        id.topPiece = Colors.None;
        id.bottomPiece = Colors.None;

        target = null;
    }

    // Update is called once per frame
    void Update() {
        // TODO:
        //  - check for input from the control room. Once input is given start looking for given description.
        //  - stop NPC's and "question" them
        //  - arrest suspect when description is complete
        //  - arrest suspect if he is found while talking to an NPC with a partial description

        if (target == null) {
            Debug.Log("Searching!");
            StartCoroutine(Search(id, Roles.Officer, 1.5f));
        } else
            QuestionSuspect(target);


        if (behaviour.inQuestioning) {
            behaviour.agent.isStopped = true;
        } else {
            behaviour.agent.isStopped = false;
        }
    }

    /// <summary>
    /// Checks if wanted identification is in the field of vision of the gameObject this function is called from.
    /// </summary>
    /// <param name="wanted"></param>
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

    private bool IsInFront(GameObject npc) {
        Vector3 directionToTarget = transform.position - npc.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        if (Mathf.Abs(angle) > 90 || Mathf.Abs(angle) > 270)
            return true;
        return false;
    }

    private void QuestionSuspect(GameObject target) {
        if (this.GetComponent<NavMeshAgent>() != null)
            this.GetComponent<NavMeshAgent>().speed = 2.5f;
        canQuestion = true;

        behaviour.MoveToTarget(target);
    }

    private IEnumerator Questioning(float time) {
        Debug.Log("Questioning suspect");
        yield return new WaitForSeconds(time);
        hasQuestioned = true;
        Debug.Log("Finished questioning");
        behaviour.inQuestioning = false;
        target.GetComponent<Collider>().GetComponent<NPCBehaviour>().inQuestioning = false;
        target = null;   
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().tag == "NPC")
            if (other.GetComponent<Collider>().tag == "NPC" && IsEqual(other.GetComponent<Collider>().GetComponent<Identification>(), lookingFor, LookFor(lookingFor)) 
                && canQuestion && other.GetComponent<NPCBehaviour>().questioned == false) {
                other.GetComponent<Collider>().GetComponent<NPCBehaviour>().inQuestioning = true;

                other.GetComponent<Collider>().GetComponent<NPCBehaviour>().officerQuestioning = this.gameObject;
                behaviour.inQuestioning = true;
                behaviour.FaceTarget(other.GetComponent<Transform>());
                StartCoroutine(Questioning(5f));
            }
    }
}
