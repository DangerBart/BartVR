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

    [SerializeField]
    private GameObject popupWindow;

    [SerializeField]
    private Image image;


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
    }

    public void SetImage(Sprite image) 
    {
        if (image == null){
            popupWindow.SetActive(false);
        }

        this.image.sprite = image;
    }
}
