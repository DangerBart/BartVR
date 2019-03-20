﻿using UnityEngine;

public class Notification{

    // Attributes
    public int Id;
    public int? ReactionTo;
    public string Autor;
    public string Message;
    public string Platform;
    public string Image;
    public bool Postable;
    public Sprite PlatformLogo;
    public Sprite Img;
    public Vector2 MinimapLocation;

    // Used to display
    public bool IsFavorite;
    public bool IsSelected;

    // To be removed
    public int POI;
    public Vector3 POILocation;
}
