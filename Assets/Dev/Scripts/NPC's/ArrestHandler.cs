using UnityEngine;

public class ArrestHandler : MonoBehaviour { 

    public GameObject gameOverText;

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Arrest collision");
        Debug.Log("Arrest in progress");
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
