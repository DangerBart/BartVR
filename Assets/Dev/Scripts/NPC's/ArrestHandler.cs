using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrestHandler : MonoBehaviour {

    public GameObject gameOverText;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Suspect") {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }
}
