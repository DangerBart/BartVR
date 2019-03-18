using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class Notification{
    public int Id;
    public int? ReactionTo;
    public string Autor;
    public string Message;
    public int POI;
    public Vector3 POILocation;
    public string Platform;
    public string Image;
    public bool Postable;
    public Sprite PlatformLogo;
    public Sprite Img;
    public bool IsFavorite;
    public bool IsSelected;
    public Vector2 MinimapLocation;
}
