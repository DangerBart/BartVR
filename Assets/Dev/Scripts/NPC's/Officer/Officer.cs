using System;
using UnityEngine;

public class Officer : MonoBehaviour {
    private GameObject npcContainer;

    Identification id = new Identification();

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
	void Start () {
        if (GameObject.Find("NPCContainer") == null)
            throw new System.Exception("No NPCContainer found, make sure the name matches in casing");
        npcContainer = GameObject.Find("NPCContainer");

        id.gender = Genders.Male;
        id.topPiece = Colors.None;
        id.bottomPiece = Colors.None;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Searching for civilians matching description: " + id.gender);
            Search(id);
        }
	}
    
    public void Search(Identification wanted) {
        foreach(Identification idToCheck in npcContainer.GetComponentsInChildren<Identification>()) {
            
            if (idToCheck.role == Roles.Civilian) {
                // raycasting information
                Vector3 npcPosition = new Vector3(idToCheck.GetComponent<Transform>().position.x, 1, idToCheck.GetComponent<Transform>().position.z);
                Vector3 ownPosition = new Vector3(this.transform.position.x, 1, this.transform.position.z);
                RaycastHit hit;
                
                switch (LookFor(wanted)) {
                    case Check.Gender:
                        if(IsEqual(wanted, idToCheck, Check.Gender)) {
                            if(Physics.Linecast(ownPosition, npcPosition, out hit)) {
                                Debug.Log("We hit: " + hit.collider.name);
                                Debug.DrawLine(ownPosition, npcPosition, Color.green, 5.0f);
                                if(hit.collider.tag == "Civilian") {
                                    Debug.Log("Approaching suspect!");
                                    Debug.Break();
                                }
                            }
                        }
                        break;
                    default:
                        Debug.Log("Found nothing boys");
                        break;
                }
            }
        }
    }
    
    private Check LookFor(Identification id) {
        char[] isSet = { '0','0','0'};

        if(id.gender != Genders.None) {
            isSet[0] = '1';
        }
        if(id.topPiece != Colors.None) {
            isSet[1] = '1';
        }
        if(id.bottomPiece != Colors.None) {
            isSet[2] = '1';
        }

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
}
