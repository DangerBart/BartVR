using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {
    [SerializeField]
    private GameObject inputRequired;
    [SerializeField]
    private InputField amountOfNpcs;
    
    enum InputSetting {
        None,
        Movement,
        Role,
        Scenario
    }

    public enum PlayableRole {
        Officer,
        Civilian
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
    public static PlayableRole currentRole = PlayableRole.Officer;
    public static Movement currentMovement = Movement.FacingDirection;
    public static Scenario currentScenario = Scenario.Mugging;

    public void StartGame() {
        // First digit has to be between 1 and 9, following digits have to be numbers
        Regex regex = new Regex(@"^[1-9]\d*$");
        Match match = regex.Match(amountOfNpcs.text);

        if (match.Success && amountOfNpcsToSpawn <= 150) {
            SceneManager.LoadScene((int)currentScenario + 1);
            //Start time
            Time.timeScale = 1;
        } else {
            inputRequired.GetComponent<Text>().text = "Vul een geldig tussen 0 en 150 getal in";
            inputRequired.SetActive(true);
        }
    }

    public void Next(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Role:
                if (currentRole == PlayableRole.Officer)
                    currentRole = PlayableRole.Civilian;
                else
                    currentRole = PlayableRole.Officer;
                SetRoleText(settingField);
                break;
            case InputSetting.Movement:
                if ((int)currentMovement < 2)
                    currentMovement++;
                else
                    currentMovement = Movement.FacingDirection;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (currentScenario == Scenario.Mugging)
                    currentScenario = Scenario.Shoplifting;
                else
                    currentScenario = Scenario.Mugging;
                SetScenarioText(settingField);
                break;
        }
    }

    public void Previous(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Role:
                if (currentRole == PlayableRole.Officer)
                    currentRole = PlayableRole.Civilian;
                else
                    currentRole = PlayableRole.Officer;
                SetRoleText(settingField);
                break;
            case InputSetting.Movement:
                if (currentMovement > 0)
                    currentMovement--;
                else
                    currentMovement = Movement.Teleport;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (currentScenario == Scenario.Mugging)
                    currentScenario = Scenario.Shoplifting;
                else
                    currentScenario = Scenario.Mugging;
                SetScenarioText(settingField);
                break;
        }
    }

    private InputSetting GetSetting(string name) {
        switch (name) {
            case "PlayerRoleInputField":
                return InputSetting.Role;
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

    private void SetRoleText(GameObject settingField) {
        switch (currentRole) {
            case PlayableRole.Civilian:
                settingField.GetComponent<InputField>().text = "Burger";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Agent";
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

    public void EnteredNPCValue() {
        amountOfNpcsToSpawn = int.Parse(amountOfNpcs.text);
    }
}
