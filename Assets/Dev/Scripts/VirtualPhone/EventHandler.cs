using System.IO;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    private readonly string pictureRoot = "Assets/Resources/Snapshots/";

    //Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }
}
