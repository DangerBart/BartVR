using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {
    private Text username;
    private Text message;
    private Image mediaPlaform;
    //image needs to be public to be able to clone
    public Sprite image;
    public GameObject panelImage;
    private GameObject imageButton;
    private Image favoriteButton;
    public GameObject notificationMenu;
    public bool isFavorite;

    public void SetGameObjects(){
        username = this.transform.Find("UserName").GetComponent<Text>();
        message = this.transform.Find("Message").GetComponent<Text>();
        mediaPlaform = this.transform.Find("MediaPlatform").GetComponent<Image>();
        favoriteButton = this.transform.Find("Favorite Button").GetComponent<Image>();
        imageButton = this.transform.Find("Show Button").gameObject;
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
    public void ShowImage() {
        panelImage.GetComponent<Image>().sprite = image;
    }
    public void ToggleFavoriteButton(){
        if(isFavorite){
            isFavorite = false;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/EmptyStar");
        }
        else{
            isFavorite = true;
            favoriteButton.sprite = Resources.Load<Sprite>("Notification/FilledStar");
        }
        notificationMenu.GetComponent<NotificationControl>().ToggleFavoritePanel(gameObject, isFavorite);
        DeletePanel();
    }

    public void DeletePanel(){
        Destroy(gameObject);
    }
}
