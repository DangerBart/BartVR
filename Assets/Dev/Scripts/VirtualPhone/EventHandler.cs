using System.IO;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    private string pictureRoot = "C:/Users/Vive/Desktop/BARTVR/BartVR/Assets/Resources/Snapshots/";

    //Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }
}
