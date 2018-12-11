using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {
    //image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
    public GameObject notificationMenu;
    public GameObject Timestamp;
    public bool isFavorite;
    private Text username;
    private Text message;
    private Image mediaPlaform;
    private GameObject imageButton;
    private Image favoriteButton;
    private Text Date;

    public void SetGameObjects() {
        imageButton = this.transform.Find("Show Button").gameObject;
    }
    public void SetComponents() {
        username = this.transform.Find("UserName").GetComponent<Text>();
        message = this.transform.Find("Message").GetComponent<Text>();
        mediaPlaform = this.transform.Find("MediaPlatform").GetComponent<Image>();
        favoriteButton = this.transform.Find("Favorite Button").GetComponent<Image>();
        Date = this.transform.Find("Date").GetComponent<Text>();
    }
    
    public void SetName(string name) {
        this.username.text = name;
    }

    public void SetMessage(string message) {
        this.message.text = message;
    }

    public void SetMediaPlatform(Sprite image) {
        this.mediaPlaform.sprite = image;
    }

    public void SetImage(Sprite img) {
        if(img != null) {
            this.image = img;
            imageButton.SetActive(true);
        }
    }

    public void SetTime(){
        Text date = Timestamp.transform.Find("Date").GetComponent<Text>();
        Text time = Timestamp.transform.Find("Time").GetComponent<Text>();
        Date.text = string.Format("{0} \n{1}", time.text, date.text);
    }

    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }

    public void ToggleFavoriteButton() {
        if(isFavorite) {
            isFavorite = false;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/EmptyStar");
        }
        else {
            isFavorite = true;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/FilledStar");
        }
        notificationMenu.GetComponent<NotificationControl>().ToggleFavoritePanel(gameObject, isFavorite);
        DeletePanel();
    }

    public void DeletePanel() {
        Destroy(gameObject);
    }
}
