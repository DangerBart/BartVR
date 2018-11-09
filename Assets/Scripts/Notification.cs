using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {
    //Set up variables needed for notifications
    public string content;
    public Image image;
    //public Location2D location;
    public string sender;
    public bool relevance;
    //public [insert time type here] time;
    public Image platform;
    private GameObject container;
    private RectTransform rect;
    private CanvasRenderer canvasRen;
    private Image img;
    private static float dynamicX = -664;
    private float dynamicY = -455;
    private float notificationWidth = 100;
    private float notificationHeight = 31;

    public Notification(GameObject notificationbar) {
        container = new GameObject("NotificationContainer");
        container.transform.SetParent(notificationbar.transform);
        rect = container.AddComponent<RectTransform>();
        canvasRen = container.AddComponent<CanvasRenderer>();
        img = container.AddComponent<Image>();
    }

    public void add() {
        rect.transform.localPosition = new Vector2(dynamicX - notificationWidth, dynamicY);
        dynamicX += 540;      
        rect.sizeDelta = new Vector2(notificationWidth, notificationHeight);
    }
}
