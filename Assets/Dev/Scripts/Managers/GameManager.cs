using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

enum InputSetting
{
    None,
    Movement,
    Mode,
    Scenario
}

public enum PlayingMode
{
    MultiplayerAM,
    MultiplayerBM,
    Singleplayer
}

public enum MovementMode
{
    FacingDirection,
    ControllerDirection,
    Teleport
}


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCValueText;
    [SerializeField]
    private Slider NPCValueSlider;
    [SerializeField]
    private GameObject miniMenu;

    private List<string> multiplayerAMScenes = new List<string>();
    private List<string> multiplayerBMScenes = new List<string>();
    private List<string> singleplayerScenes = new List<string>();


    public static int amountOfMultiplayerAMScenes;
    public static int amountOfMultiplayerBMScenes;
    public static int amountOfNpcsToSpawn;
    public static PlayingMode currentMode = PlayingMode.MultiplayerBM;
    public static MovementMode currentMovement = MovementMode.FacingDirection;
    public static int currentScenario;
    public static bool DesktopMode = true;

    public void StartGame()
    {
        if (currentMode == PlayingMode.MultiplayerAM)//Multiplayer Agent + Meldkamer
        {
            SceneManager.LoadScene(currentScenario + 1);
        }
        else if (currentMode == PlayingMode.MultiplayerBM)//Multiplayer Burger + Meldkamer
        {
            SceneManager.LoadScene(currentScenario + 1);
        }
        else if (currentMode == PlayingMode.Singleplayer)//Singleplayer
        {
            SceneManager.LoadScene(singleplayerScenes.Count + currentScenario + 1);
        }
        //Start time
        Time.timeScale = 1;
    }

    private void Awake()
    {
        // Set initial value to 1
        amountOfNpcsToSpawn = (int)FindObjectOfType<Slider>().value;

        // ----------------------------------- CHANGE TO CHECK MODEL NAME FOR QUEST --------------------------------
        if (!XRDevice.model.ToLower().Contains("vive") && !XRDevice.model.ToLower().Contains("cv") && !XRDevice.model.ToLower().Contains("rift"))
        {
            miniMenu.SetActive(true);
            DesktopMode = false;
        }

        DirectoryInfo multiDIAM = new DirectoryInfo("Assets/Scenes/Multiplayer Scenes (Agent + Meldkamer)");
        DirectoryInfo multiDIBM = new DirectoryInfo("Assets/Scenes/Multiplayer (Burger + Meldkamer)");
        DirectoryInfo singleDI = new DirectoryInfo("Assets/Scenes/Singleplayer Scenes");

        foreach (FileInfo file in multiDIAM.GetFiles("*.unity"))
        {
            string scene = file.Name.Replace(".unity", "");
            multiplayerAMScenes.Add(scene);
        }

        amountOfMultiplayerAMScenes = multiplayerAMScenes.Count;

        foreach (FileInfo file in multiDIBM.GetFiles("*.unity"))
        {
            string scene = file.Name.Replace(".unity", "");
            multiplayerBMScenes.Add(scene);
        }

        amountOfMultiplayerBMScenes = multiplayerBMScenes.Count;

        foreach (FileInfo file in singleDI.GetFiles("*.unity"))
        {
            string scene = file.Name.Replace(".unity", "");
            singleplayerScenes.Add(scene);
        }
    }

    public void Next(GameObject settingField)
    {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting)
        {
            case InputSetting.Mode:
                if ((int)currentMode < Enum.GetNames(typeof(PlayingMode)).Length - 1)
                    currentMode++;
                else
                    currentMode = 0;
                SetModeText(settingField);
                break;
            case InputSetting.Movement:
                if ((int)currentMovement < Enum.GetNames(typeof(MovementMode)).Length - 1)
                    currentMovement++;
                else
                    currentMovement = 0;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (currentMode == PlayingMode.MultiplayerAM)
                {
                    if (currentScenario < multiplayerAMScenes.Count - 1)
                        currentScenario++;
                    else
                        currentScenario = 0;
                    settingField.GetComponent<InputField>().text = multiplayerAMScenes[currentScenario];
                }
                else if (currentMode == PlayingMode.MultiplayerBM)
                {
                    if (currentScenario < multiplayerBMScenes.Count - 1)
                    {
                        currentScenario++;
                    }
                    else
                    {
                        currentScenario = 0;
                    }
                    settingField.GetComponent<InputField>().text = multiplayerBMScenes[currentScenario];
                }
                else
                {
                    if (currentScenario < singleplayerScenes.Count - 1)
                        currentScenario++;
                    else
                        currentScenario = 0;
                    settingField.GetComponent<InputField>().text = singleplayerScenes[currentScenario];
                }
                break;
        }
    }

    public void Previous(GameObject settingField)
    {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting)
        {
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
                    currentMovement = (MovementMode)Enum.GetNames(typeof(MovementMode)).Length - 1;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (currentMode == PlayingMode.MultiplayerAM)
                {
                    if (currentScenario > 0)
                        currentScenario--;
                    else
                        currentScenario = multiplayerAMScenes.Count - 1;
                    settingField.GetComponent<InputField>().text = multiplayerAMScenes[currentScenario];
                }
                else if (currentMode == PlayingMode.MultiplayerBM)
                {
                    if (currentScenario > 0)
                        currentScenario--;
                    else
                        currentScenario = multiplayerBMScenes.Count - 1;
                    settingField.GetComponent<InputField>().text = multiplayerBMScenes[currentScenario];
                }
                else
                {
                    if (currentScenario > 0)
                        currentScenario--;
                    else
                        currentScenario = singleplayerScenes.Count - 1;
                    settingField.GetComponent<InputField>().text = singleplayerScenes[currentScenario];
                }
                break;
        }
    }

    public void IncrementSlider(Slider slider)
    {
        slider.value += 10;
    }

    public void DecrementSlider(Slider slider)
    {
        slider.value -= 10;
    }

    private InputSetting GetSetting(string name)
    {
        switch (name)
        {
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

    private void SetMovementText(GameObject settingField)
    {
        switch (currentMovement)
        {
            case MovementMode.ControllerDirection:
                settingField.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case MovementMode.Teleport:
                settingField.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    private void SetModeText(GameObject settingField)
    {
        switch (currentMode)
        {
            case PlayingMode.Singleplayer:
                settingField.GetComponent<InputField>().text = "Singleplayer";
                break;
            case PlayingMode.MultiplayerAM:
                settingField.GetComponent<InputField>().text = "Multiplayer agent";
                break;
            case PlayingMode.MultiplayerBM:
                settingField.GetComponent<InputField>().text = "Multiplayer burger";
                break;
        }
    }

    public void ChangedNPCValue()
    {
        NPCValueText.GetComponent<Text>().text = NPCValueSlider.value.ToString();
        amountOfNpcsToSpawn = (int)NPCValueSlider.value;
    }
}
