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

    public enum Roles {
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
    public static Roles currentRole = Roles.Officer;
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

    public void Next(GameObject go) {
        InputSetting setting = GetSetting(go.name);

        switch (setting) {
            case InputSetting.Role:
                if (currentRole == Roles.Officer)
                    currentRole = Roles.Civilian;
                else
                    currentRole = Roles.Officer;
                SetRoleText(go);
                break;
            case InputSetting.Movement:
                if ((int)currentMovement < 2)
                    currentMovement++;
                else
                    currentMovement = Movement.FacingDirection;
                SetMovementText(go);
                break;
            case InputSetting.Scenario:
                if (currentScenario == Scenario.Mugging)
                    currentScenario = Scenario.Shoplifting;
                else
                    currentScenario = Scenario.Mugging;
                SetScenarioText(go);
                break;
        }
    }

    public void Previous(GameObject go) {
        InputSetting setting = GetSetting(go.name);

        switch (setting) {
            case InputSetting.Role:
                if (currentRole == Roles.Officer)
                    currentRole = Roles.Civilian;
                else
                    currentRole = Roles.Officer;
                SetRoleText(go);
                break;
            case InputSetting.Movement:
                if (currentMovement > 0)
                    currentMovement--;
                else
                    currentMovement = Movement.Teleport;
                SetMovementText(go);
                break;
            case InputSetting.Scenario:
                if (currentScenario == Scenario.Mugging)
                    currentScenario = Scenario.Shoplifting;
                else
                    currentScenario = Scenario.Mugging;
                SetScenarioText(go);
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

    private void SetMovementText(GameObject go) {
        switch (currentMovement) {
            case Movement.ControllerDirection:
                go.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case Movement.Teleport:
                go.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                go.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    private void SetRoleText(GameObject go) {
        switch (currentRole) {
            case Roles.Civilian:
                go.GetComponent<InputField>().text = "Burger";
                break;
            default:
                go.GetComponent<InputField>().text = "Agent";
                break;
        }
    }

    private void SetScenarioText(GameObject go) {
        switch (currentScenario) {
            case Scenario.Shoplifting:
                go.GetComponent<InputField>().text = "Winkeldiefstal";
                break;
            default:
                go.GetComponent<InputField>().text = "Straatroof";
                break;
        }
    }

    public void EnteredNPCValue() {
        amountOfNpcsToSpawn = int.Parse(amountOfNpcs.text);
    }
}
