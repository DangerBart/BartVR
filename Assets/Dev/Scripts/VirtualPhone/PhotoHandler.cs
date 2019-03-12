using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoHandler : MonoBehaviour {

    private string pictureRoot = "C:/Users/Vive/Desktop/BARTVR/BartVR/Assets/Resources/Snapshots/";
    string tempPath = "C:/Users/Vive/Desktop/BARTVR/BartVR/Assets/Resources/Snapshots/screenshot.png";

    private int pictureID = 0;

    public IEnumerator TakeScreenShot(GameObject cam, GameObject preview, GameObject confirmPanel) {
        yield return new WaitForEndOfFrame();

        string path;


        // This code stops the rendertexture for a short moment so it can take all the pixels on it and turn it back on
        // Once the pixels are collected they are put onto a Texture2D which is saved in screenshot.png
        Camera camOV = cam.GetComponent<Camera>();
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = camOV.targetTexture;
        camOV.Render();
        Texture2D imageOverview = new Texture2D(camOV.targetTexture.width,
                                                camOV.targetTexture.height, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height), 0, 0);
        imageOverview.Apply();
        RenderTexture.active = currentRT;

        // Encode texture into PNG
        byte[] bytes = imageOverview.EncodeToPNG();

        // save in memory
        string filename = "screenshot.png";
        path = pictureRoot + filename;
        // Write to path (previous screenshots are overwritten)
        File.WriteAllBytes(path, bytes);
        SetPreview(preview, confirmPanel);
    }

    private void SetPreview(GameObject preview, GameObject confirmPanel) {
        // Turn the screenshot into a sprite and set it onto the preview panel
        Sprite previewSprite = MakeSprite();
        preview.GetComponent<Image>().sprite = previewSprite;
        // Ask if player wants to send this picture to the OC
        confirmPanel.SetActive(true);
    }

    private Sprite MakeSprite() {
        // Turn the Texture2D from the screenshot into a sprite so it can be loaded for preview
        Sprite sprite;
        Texture2D spriteTexture = LoadTexture(pictureRoot + "screenshot.png");
        sprite = Sprite.Create(spriteTexture, new Rect(0, 0, 500, 1000), new Vector2(0, 0), 100f, 0, SpriteMeshType.Tight);

        return sprite;
    }

    private Texture2D LoadTexture(string FilePath) {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath)) {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);            // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))          // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                       // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public void SendPictureToOC() {
        string newPath = string.Format(pictureRoot + "OCpicture{0}.png", pictureID);
        pictureID++;
        if (File.Exists(tempPath))
            File.Move(tempPath, newPath);
    }

    //Delete screenshots after application quit
    private void OnApplicationQuit() {
        DirectoryInfo di = new DirectoryInfo(pictureRoot);

        foreach (FileInfo file in di.GetFiles())
            file.Delete();
    }
}
