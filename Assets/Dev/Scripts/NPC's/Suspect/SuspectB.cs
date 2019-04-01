using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectB : MonoBehaviour {
    private Identification cop;
    private Officer off;

    void Start() {
        cop = new Identification();
        off = new Officer();

        cop.role = Roles.Officer;
        cop.gender = Genders.None;
        cop.topPiece = Colors.None;
        cop.bottomPiece = Colors.None;
    }

    // Update is called once per frame
    void Update () {
        //StartCoroutine(off.Search(cop, this.GetComponent<Identification>().role, 2f));
	}
}
