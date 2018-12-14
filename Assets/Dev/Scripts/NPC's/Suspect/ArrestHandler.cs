using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrestHandler : MonoBehaviour { 

    public GameObject gameOverText;
    public GameObject gameOverScreen;

    //When player enters within the radius of the suspect during the final POI, stop time and display game over
    void OnCollisionEnter(Collision collision) {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        gameOverScreen.SetActive(true);
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public void EndGame() {
        SceneManager.LoadScene(0);
    }
}
