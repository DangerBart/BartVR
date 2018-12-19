using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

	public void StartGame() {
        // First digit has to be between 1 and 9, following digits have to be numbers
        Regex regex = new Regex(@"^[1-9]\d*$");
        Match match = regex.Match(amountOfNpcs.text);

        if (match.Success && amountOfNpcsToSpawn <= 150) {
            SceneManager.LoadScene(1);
            //Start time
            Time.timeScale = 1;
        } else {
            inputRequired.GetComponent<Text>().text = "Vul een geldig tussen 0 en 150 getal in";
            inputRequired.SetActive(true);
        }
    }

    public void UpdateMovement() {
        movementValue = movement.value;
    }

    public void EnteredNPCValue() {
        amountOfNpcsToSpawn = int.Parse(amountOfNpcs.text);
    }
}
