using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    private readonly string pictureRoot = "Assets/Resources/Snapshots/";

    public static GameObject gameOverScreen;
    public static GameObject gameOverText;

    [SerializeField]
    private GameObject gameOverS;
    [SerializeField]
    private GameObject gameOverT;
    [SerializeField]
    private GameObject escapeMenu;

    private void Start() {
        gameOverScreen = gameOverS;
        gameOverText = gameOverT;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (escapeMenu.activeInHierarchy) {
                ReturnToGame();
                escapeMenu.SetActive(false);
            } else {
                Time.timeScale = 0;
                escapeMenu.SetActive(true);
            }
        }
    }

    //Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        if (di == null)
            return;
        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }

    public void ReturnToGame() {
        escapeMenu.SetActive(false);
        FindObjectOfType<Movement>().UpdateMovement();
    }

    public void Next(GameObject movementSelection) {
        if ((int)GameManager.currentMovement < 2)
            GameManager.currentMovement++;
        else
            GameManager.currentMovement = GameManager.Movement.FacingDirection;
        SetMovementText(movementSelection);
    }

    public void Previous(GameObject movementSelection) {
        if (GameManager.currentMovement > 0)
            GameManager.currentMovement--;
        else
            GameManager.currentMovement = GameManager.Movement.Teleport;
        SetMovementText(movementSelection);
    }

    private void SetMovementText(GameObject go) {
        switch (GameManager.currentMovement) {
            case GameManager.Movement.ControllerDirection:
                go.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case GameManager.Movement.Teleport:
                go.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                go.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    public static void End() {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        gameOverScreen.SetActive(true);
    }
}
