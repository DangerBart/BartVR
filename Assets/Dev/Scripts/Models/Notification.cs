using UnityEngine;

public enum KindOfNotification {
    Relevant,
    Irrelevant,
    Postable
}

public class Notification{

    // Attributes
    public int Id;
    public int? ReactionTo;
    public string Autor;
    public string Message;
    public string Platform;
    public string PostTime;
    public string Image;
    public bool Postable;
    public bool ReactionOfPostableNotif;
    public Sprite PlatformLogo;
    public Sprite Img;
    public Vector2 MinimapLocation;

    // Used to display
    public bool IsFavorite;
    public bool IsSelected;
    public bool WaitingForPost;
}
