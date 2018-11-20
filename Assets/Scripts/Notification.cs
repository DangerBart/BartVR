using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class Notification{
    public string Voornaam;
    public string Achternaam;
    public string Bericht;
    public int POI;
    public string Mediaplatform;


    ////Set up shape for notifications
    //private GameObject container;
    //private RectTransform rect;
    //private CanvasRenderer canvasRen;

    ////Set up shape for notifications
    //private static float dynamicX = -664;
    //private float dynamicY = -455;
    //private float notificationWidth = 100;
    //private float notificationHeight = 31;

    //public Notification(GameObject notificationbar, string message, bool relevance)
    //{
    //}

    //private void CreateShape(GameObject notificationbar)
    //{
    //    container = new GameObject("NotificationContainer");
    //    container.transform.SetParent(notificationbar.transform);
    //    rect = container.AddComponent<RectTransform>();
    //    canvasRen = container.AddComponent<CanvasRenderer>();
    //}
    //public void add() {
    //    rect.transform.localPosition = new Vector2(dynamicX - notificationWidth, dynamicY);
    //    dynamicX += 540;      
    //    rect.sizeDelta = new Vector2(notificationWidth, notificationHeight);
    //}
}
