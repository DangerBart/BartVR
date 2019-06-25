using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrestHandler : MonoBehaviour { 

    private GameObject gameOverText;
    private GameObject gameOverScreen;
    private GameObject SPGameOverScreen;

    void Awake() {
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverText = GameObject.Find("GameOverText");
        SPGameOverScreen = GameObject.Find("SPGameOverScreen");

        if (GameManager.currentMode == PlayingMode.Multiplayer) {
            gameOverScreen.SetActive(false);
            gameOverText.SetActive(false);
        } else 
            SPGameOverScreen.SetActive(false);
    }

    //When player enters within the radius of the suspect, stop time and display game over
    void OnCollisionEnter(Collision collision) {
        Debug.Log("Arrested by: " + collision.collider + "\nFound on: " + collision.collider.name);
        Time.timeScale = 0;
        if (GameManager.currentMode == PlayingMode.Multiplayer) {
            gameOverText.SetActive(true);
            gameOverScreen.SetActive(true);
        } else {
            GameObject.Find("RightHandAnchor").GetComponent<OVRUILaser>().enabled = true;
            GameObject.Find("RightHandAnchor").GetComponent<SteamVR_LaserPointer>().enabled = true;
            SPGameOverScreen.SetActive(true);
        }
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public static void EndGame() {
        SceneManager.LoadScene(0);
    }
}
