using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {

    [SerializeField]
    private Dropdown movement;
    [SerializeField]
    private Dropdown teleportingHand;

	public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public int GetMovementStyle() {
        return movement.value;
    }
}
