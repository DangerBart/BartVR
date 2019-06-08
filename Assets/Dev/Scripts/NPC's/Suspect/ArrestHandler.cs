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

        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);
        SPGameOverScreen.SetActive(false);
    }

    //When player enters within the radius of the suspect during the final POI, stop time and display game over
    void OnCollisionEnter(Collision collision) {
        Time.timeScale = 0;
        if (GameManager.currentMode == PlayingMode.Multiplayer) {
            gameOverText.SetActive(true);
            gameOverScreen.SetActive(true);
        } else {
            SPGameOverScreen.SetActive(true);
        }
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public static void EndGame() {
        SceneManager.LoadScene(0);
    }
}
