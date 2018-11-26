using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationButton : MonoBehaviour {

    [SerializeField]
    private Text name;

    [SerializeField]
    private Text message;

    [SerializeField]
    private Image mediaPlaform;

    public void SetName(string name)
    {
        this.name.text = name;
    }

    public void SetMessage(string message) 
    {
        this.message.text = message;
    }

    public void SetMediaPlatform(Sprite image)
    {
        this.mediaPlaform.sprite = image;
        Debug.Log("sprite: " + image.ToString());
    }
}
