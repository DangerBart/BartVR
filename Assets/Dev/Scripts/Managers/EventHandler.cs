using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    private readonly string pictureRoot = "Assets/Resources/Snapshots/";

    [SerializeField]
    private GameObject escapeMenu;

    private void Update() {
        // Check if menu needs to be opened
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

    // Delete screenshots after application quit
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
        Time.timeScale = 1;
        escapeMenu.SetActive(false);
        FindObjectOfType<Movement>().UpdateMovement();
    }

    public void Next(GameObject movementSelection) {
        if ((int)GameManager.currentMovement < 2)
            GameManager.currentMovement++;
        else
            GameManager.currentMovement = MovementMode.FacingDirection;
        SetMovementText(movementSelection);
    }

    public void Previous(GameObject movementSelection) {
        if (GameManager.currentMovement > 0)
            GameManager.currentMovement--;
        else
            GameManager.currentMovement =MovementMode.Teleport;
        SetMovementText(movementSelection);
    }

    private void SetMovementText(GameObject go) {
        switch (GameManager.currentMovement) {
            case MovementMode.ControllerDirection:
                go.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case MovementMode.Teleport:
                go.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                go.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    public void End() {
        ArrestHandler.EndGame();
    }

    public void ArrestedSuspect() {
        ArrestHandler.ActivateGameOverScreen();
    }
}
