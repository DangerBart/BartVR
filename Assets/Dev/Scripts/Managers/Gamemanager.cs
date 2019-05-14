using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject NPCValueText;
    [SerializeField]
    private Slider NPCValueSlider;
    
    enum InputSetting {
        None,
        Movement,
        Mode,
        Scenario
    }

    public enum PlayingMode {
        Multiplayer,
        Singleplayer
    }

    public enum Movement {
        FacingDirection,
        ControllerDirection,
        Teleport
    }

    public enum Scenario {
        Mugging,
        Shoplifting
    }

    public static int amountOfNpcsToSpawn;
    public static PlayingMode currentMode = PlayingMode.Multiplayer;
    public static Movement currentMovement = Movement.FacingDirection;
    public static Scenario currentScenario = Scenario.Mugging;

    private int multiplayerScenes;
    private int singleplayerScenes;

    public void StartGame() {
        if (amountOfNpcsToSpawn > 0) {
            SceneManager.LoadScene((int)currentScenario + 1);
            //Start time
            Time.timeScale = 1;
        }
    }

    public void StartSingleplayerGame() {
        SceneManager.LoadScene((int)currentScenario + 1);
        //Start time
        Time.timeScale = 1;
    }

    private void Awake() {
        // READ AMOUNT OF SCENARIO'S AND ADD THEM TO A LIST AND LOOP THROUGH LIST IN MENU OPTIONS
        Debug.Log("Awake, hmd in use is: " + XRDevice.model);
        DirectoryInfo multiDI = new DirectoryInfo("Scenes/Multiplayer Scenes");
        DirectoryInfo singleDI = new DirectoryInfo("Scenes/Singleplayer Scenes");

        foreach(FileInfo file in multiDI.GetFiles()) {
            multiplayerScenes++;
        }

        foreach(FileInfo file in singleDI.GetFiles()) {
            singleplayerScenes++;
        }
    }

    public void Next(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Mode:
                if ((int)currentMode < Enum.GetNames(typeof(PlayingMode)).Length - 1)
                    currentMode++;
                else
                    currentMode = 0;
                SetModeText(settingField);
                break;
            case InputSetting.Movement:
                if ((int)currentMovement < Enum.GetNames(typeof(Movement)).Length - 1)
                    currentMovement++;
                else
                    currentMovement = 0;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if ((int)currentScenario < Enum.GetNames(typeof(Scenario)).Length - 1)
                    currentScenario++;
                else
                    currentScenario = 0;
                SetScenarioText(settingField);
                break;
        }
    }

    public void Previous(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Mode:
                if (currentMode > 0)
                    currentMode--;
                else
                    currentMode = (PlayingMode)Enum.GetNames(typeof(PlayingMode)).Length - 1;
                SetModeText(settingField);
                break;
            case InputSetting.Movement:
                if (currentMovement > 0)
                    currentMovement--;
                else
                    currentMovement = (Movement)Enum.GetNames(typeof(Movement)).Length - 1;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (currentScenario > 0)
                    currentScenario--;
                else
                    currentScenario = (Scenario)Enum.GetNames(typeof(Scenario)).Length - 1;
                SetScenarioText(settingField);
                break;
        }
    }

    private InputSetting GetSetting(string name) {
        switch (name) {
            case "PlayingModeInputField":
                return InputSetting.Mode;
            case "MovementInputField":
                return InputSetting.Movement;
            case "ScenarioInputField":
                return InputSetting.Scenario;
            default:
                return InputSetting.None;
        }
    }

    private void SetMovementText(GameObject settingField) {
        switch (currentMovement) {
            case Movement.ControllerDirection:
                settingField.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case Movement.Teleport:
                settingField.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    private void SetModeText(GameObject settingField) {
        switch (currentMode) {
            case PlayingMode.Singleplayer:
                settingField.GetComponent<InputField>().text = "Singleplayer";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Multiplayer";
                break;
        }
    }

    private void SetScenarioText(GameObject settingField) {
        switch (currentScenario) {
            case Scenario.Shoplifting:
                settingField.GetComponent<InputField>().text = "Winkeldiefstal";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Straatroof";
                break;
        }
    }

    public void ChangedNPCValue() {
        NPCValueText.GetComponent<Text>().text = NPCValueSlider.value.ToString();
        amountOfNpcsToSpawn = (int) NPCValueSlider.value;
    }
}
