using System.IO;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    private readonly string pictureRoot = "Assets/Resources/Snapshots/";
    
    public static GameObject gameOverScreen;
    public static GameObject gameOverText;

    [SerializeField]
    private GameObject gameOverS;
    [SerializeField]
    private GameObject gameOverT;

    private void Start() {
        gameOverScreen = gameOverS;
        gameOverText = gameOverT;
    }

    //Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }

    public static void End() {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        gameOverScreen.SetActive(true);
    }
}
