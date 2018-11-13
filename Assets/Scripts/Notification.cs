using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {
    //Set up variables needed for notifications
    static System.Random rnd = new System.Random();
    private string sender;
    private string message;
    private bool relevance;
    private GameObject container;
    private RectTransform rect;
    private CanvasRenderer canvasRen;

    private static float dynamicX = -664;
    private float dynamicY = -455;
    private float notificationWidth = 100;
    private float notificationHeight = 31;

    public Notification(GameObject notificationbar, string message, bool relevance)
    {
        CreateShape(notificationbar);
        this.sender = CreateSender();
        Debug.Log(this.sender);
        this.message = message;
        this.relevance = relevance;
        add();
    }
    private string CreateSender()
    {
        int offset = rnd.Next(0,5);
        int firstnameID = rnd.Next(rnd.Next(0,offset), rnd.Next(firstname.Length-offset,firstname.Length));
        int lastnameID = rnd.Next(rnd.Next(0,offset), rnd.Next(lastname.Length-offset,lastname.Length));
        return ((string)firstname[firstnameID] + " " + (string)lastname[lastnameID]);
    }

    private void CreateShape(GameObject notificationbar)
    {
        container = new GameObject("NotificationContainer");
        container.transform.SetParent(notificationbar.transform);
        rect = container.AddComponent<RectTransform>();
        canvasRen = container.AddComponent<CanvasRenderer>();
    }
    public void add() {
        rect.transform.localPosition = new Vector2(dynamicX - notificationWidth, dynamicY);
        dynamicX += 540;      
        rect.sizeDelta = new Vector2(notificationWidth, notificationHeight);
    }
}
