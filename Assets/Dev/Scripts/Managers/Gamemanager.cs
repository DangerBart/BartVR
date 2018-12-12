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

	public void StartGame() {
        if (amountOfNpcs.text != "") {
            SceneManager.LoadScene(1);
        } else {
            inputRequired.SetActive(true);
        }
    }

    void Update() {
        movementValue = movement.value;
    }

    public void EnteredNPCValue() {
        amountOfNpcsToSpawn = int.Parse(amountOfNpcs.text);
    }
}
