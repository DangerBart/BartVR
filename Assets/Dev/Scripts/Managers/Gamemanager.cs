using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {
    [SerializeField]
    private Dropdown movement;
    [SerializeField]
    private GameObject inputRequired;
    [SerializeField]
    private InputField amountOfNpcs;

    public static int movementValue;
    public static int amountOfNpcsToSpawn;
    private NotificationTimer notificationTimer = new NotificationTimer();

	public void StartGame() {
        if (amountOfNpcs.text != "" && amountOfNpcs.text != "-" && amountOfNpcsToSpawn > 0 && amountOfNpcsToSpawn <= 250) {
            SceneManager.LoadScene(1);
            //Start time
            Time.timeScale = 1;
        } else {
            inputRequired.GetComponent<Text>().text = "Vul een geldig tussen 0 en 250 getal in";
            inputRequired.SetActive(true);
        }
    }

    void Update() {
            movementValue = movement.value;
    }

    public void EnteredNPCValue() {
        if (amountOfNpcs.text != "-") {
            amountOfNpcsToSpawn = int.Parse(amountOfNpcs.text);
        }
    }
}
